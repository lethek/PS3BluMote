﻿/*
Copyright (c) 2011 Ben Barron

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is furnished 
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Hibernation Code provided and integrated by Miljbee (miljbee@gmail.com)
*/

using System;
using System.Threading;
using System.Windows.Forms;

using WindowsAPI;

// OSD
using System.Drawing;
using System.Text;

// get active window title (DllImport)
using System.Runtime.InteropServices;

using System.Text.RegularExpressions;
using Microsoft.Win32;
using NLog;

using Timer = System.Threading.Timer;

namespace PS3BluMote
{
    public partial class SettingsForm : Form
    {
        # region ### fields ###
        private readonly String SETTINGS_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PS3BluMote\\";

	    private PS3RemoteService remoteService;
        private SendInputAPI.Keyboard keyboard;
        private Timer timerRepeat;
		private bool repeating;

        public ModelXml model;

        private OsdWindow osd;
        private OsdWindow.OsdTextAlign osdAlign;
        private OsdWindow.OsdVerticalAlign osdVAlign;
        private Font osdTextFont;
        private Color osdTextColor;
        private Color osdPathColor;
        private Single osdPathWidth;
        private Rectangle rScreen = Screen.PrimaryScreen.Bounds;

		//Calculate approximately (because apparently it varies a little from hardware to hardware) the system's current keyboard delay and repeat rates
		private const double KeyboardDelayX = 3d;
		private const double KeyboardDelayC = 250d;
		private const double KeyboardRepeatX = (100 / 93d - 400 / 31d);
		private const double KeyboardRepeatC = 400d;
		private static readonly int KeyboardDelay = (int)(SystemInformation.KeyboardDelay * KeyboardDelayX + KeyboardDelayC);
		private static readonly int KeyboardRepeat = (int)(SystemInformation.KeyboardSpeed * KeyboardRepeatX + KeyboardRepeatC);
		# endregion

		public SettingsForm(ModelXml model, PS3RemoteService remoteService)
        {
            InitializeComponent();

			this.model = model;
			this.remoteService = remoteService;
			remoteService.OnBatteryLifeChanged += remoteService_OnBatteryLifeChanged;
			remoteService.OnConnected += remoteService_OnConnected;
			remoteService.OnDisconnected += remoteService_OnDisconnected;
	        remoteService.OnButtonDown += remoteService_OnButtonDown;
			remoteService.OnButtonReleased += remoteService_OnButtonReleased;

			SetForms();

			SetLoggingEnabled(model.Settings.debug);

            keyboard = new SendInputAPI.Keyboard(cbSms.Checked);

			timerRepeat = new Timer(s => {
				keyboard.sendKeysDown(keyboard.lastKeysDown);
				keyboard.releaseLastKeys();
			}, null, Timeout.Infinite, Timeout.Infinite);

			var vendorId = int.Parse(txtVendorId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
			var productId = int.Parse(txtProductId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
			remoteService.SetRemote(vendorId, productId);

            if (cbOsdAppStart.Checked) ShowOsd("PS3BluMote is started");
        }

        # region ### init ###

        private void SetForms() // should use data bind?
        {
            foreach (AppNode app in model.Mappings)
            {
                lbApps.Items.Add(app.name);
            }

            cbSms.Checked = model.Settings.smsinput;
            cbDebugMode.Checked = model.Settings.debug;

			var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			if (key.GetValue("PS3BluMote") != null && key.GetValue("PS3BluMote").Equals(System.Reflection.Assembly.GetEntryAssembly().Location)) {
				cbStartup.Checked = true;
			}

	        cbCustomRepeatRate.Checked = model.Settings.usecustomdelay;
	        txtRepeatDelay.Text = model.Settings.repeatdelay.ToString();
	        txtRepeatDelay.Enabled = model.Settings.usecustomdelay;
            txtRepeatInterval.Text = model.Settings.repeatinterval.ToString();
			txtRepeatInterval.Enabled = model.Settings.usecustomdelay;

            txtVendorId.Text = model.Settings.vendorid;
            txtProductId.Text = model.Settings.productid;

            /*
            if (cbHibernation.Checked &&
                !(new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)))
            {
                MessageBox.Show("Admin/UAC elevated rights are required to use the hibernation feature! Please enable them!", "PS3BluMote: No admin rights found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            */

            // ----- OSD -----
            if (model.Settings.osd == true)
            {
                osd = new OsdWindow();
                osdAlign = new OsdWindow.OsdTextAlign();
                osdVAlign = new OsdWindow.OsdVerticalAlign();
                osdTextFont = new Font("", 16);
                osdTextColor = new Color();
                osdPathColor = new Color();
                osdPathWidth = new Single();
            }
            /*
            else
            {
                osd.Close();
                GC.Collect();
            }
            */

            cbOSD.Checked = model.Settings.osd;

            nudX.Maximum = rScreen.Width;
            nudY.Maximum = rScreen.Height;

            osdAlign = (OsdWindow.OsdTextAlign)Enum.Parse(typeof(OsdWindow.OsdTextAlign), model.OSD.align);
            osdVAlign = (OsdWindow.OsdVerticalAlign)Enum.Parse(typeof(OsdWindow.OsdVerticalAlign), model.OSD.valign);

            if (model.OSD.align == "Left")
                rbLeft.Checked = true;
            else if (model.OSD.align == "Right")
                rbRight.Checked = true;
            else
                rbCenter.Checked = true;

            if (model.OSD.valign == "Top")
                rbTop.Checked = true;
            else if (model.OSD.valign == "Middle")
                rbMiddle.Checked = true;
            else
                rbBottom.Checked = true;

            cbXYSetting.Checked = model.OSD.xysetting;
            nudX.Value = model.OSD.pos_x;
            nudY.Value = model.OSD.pos_y;

            osdTextFont = new Font(model.OSD.fontFamily, model.OSD.fontSize, (FontStyle)Enum.Parse(typeof(FontStyle), model.OSD.fontStyle));
            osdTextColor = ColorTranslator.FromHtml(model.OSD.textColor);
            osdPathColor = ColorTranslator.FromHtml(model.OSD.pathColor);
            osdPathWidth = model.OSD.pathWidth;

            nudAlpha.Value = model.OSD.alpha;
            nudTextTime.Value = model.OSD.textTime;
            nudAnimateTime.Value = model.OSD.animateTime;
            cbAnimateEffect.Items.AddRange(Enum.GetNames(typeof(OsdWindow.AnimateMode)));
            cbAnimateEffect.SelectedIndex = (int)Enum.Parse(typeof(OsdWindow.AnimateMode), model.OSD.animateEffect);

            cbOsdAppStart.Checked = model.OSD.osdWhen.appStart;
            cbOsdRemoteConnect.Checked = model.OSD.osdWhen.remoteConnect;
            cbOsdRemoteDisconnect.Checked = model.OSD.osdWhen.remoteDisconnect;
            cbOsdRemoteHibernate.Checked = model.OSD.osdWhen.remoteHibernate;
            cbOsdRemoteBatteryChange.Checked = model.OSD.osdWhen.remoteBatteryChange;

            cbOsdRemoteButtonPress.Checked = model.OSD.osdWhen.remoteButtonPressed;
            rbOsdRemoteButtonPressAlways.Checked = model.OSD.osdWhen.remoteButtonPressedAlways;
            rbOsdRemoteButtonPressMatched.Checked = model.OSD.osdWhen.remoteButtonPressedMatched;
            rbOsdRemoteButtonPressAssigned.Checked = model.OSD.osdWhen.remoteButtonPressedAssigned;

            cbOsdActiveWindowTitle.Checked = model.OSD.osdWhen.activeWindowTitle;
            cbOsdMappingName.Checked = model.OSD.osdWhen.mappingName;
            cbOsdPressedRemoteButton.Checked = model.OSD.osdWhen.pressedRemoteButton;
            cbOsdAssignedKey.Checked = model.OSD.osdWhen.assignedKey;

            txtTestString.Text = model.OSD.testString;

            // -----
            rbLeft.Enabled = cbOSD.Checked;
            rbCenter.Enabled = cbOSD.Checked;
            rbRight.Enabled = cbOSD.Checked;

            rbTop.Enabled = cbOSD.Checked;
            rbMiddle.Enabled = cbOSD.Checked;
            rbBottom.Enabled = cbOSD.Checked;

            cbXYSetting.Enabled = cbOSD.Checked;
            nudX.Enabled = cbOSD.Checked;
            nudY.Enabled = cbOSD.Checked;

            buttonFontPick.Enabled = cbOSD.Checked;
            buttonTextColorPick.Enabled = cbOSD.Checked;
            buttonPathColorPick.Enabled = cbOSD.Checked;

            nudAlpha.Enabled = cbOSD.Checked;
            nudTextTime.Enabled = cbOSD.Checked;
            cbAnimateEffect.Enabled = cbOSD.Checked;
            nudAnimateTime.Enabled = cbOSD.Checked;

            cbOsdAppStart.Enabled = cbOSD.Checked;
            cbOsdRemoteConnect.Enabled = cbOSD.Checked;
            cbOsdRemoteDisconnect.Enabled = cbOSD.Checked;
            cbOsdRemoteHibernate.Enabled = cbOSD.Checked;
            cbOsdRemoteBatteryChange.Enabled = cbOSD.Checked;

            cbOsdRemoteButtonPress.Enabled = cbOSD.Checked;
            rbOsdRemoteButtonPressAlways.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressMatched.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressAssigned.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;

            cbOsdActiveWindowTitle.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdMappingName.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdPressedRemoteButton.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdAssignedKey.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;

            txtTestString.Enabled = cbOSD.Checked;
            buttonTestOsd.Enabled = cbOSD.Checked;
        }

        private void saveSettings()
        {
            // should use data bind?
            model.Settings.vendorid = txtVendorId.Text.ToLower();
            model.Settings.productid = txtProductId.Text.ToLower();
            model.Settings.smsinput = cbSms.Checked;
	        model.Settings.usecustomdelay = cbCustomRepeatRate.Checked;
	        model.Settings.repeatdelay = int.Parse(txtRepeatDelay.Text);
            model.Settings.repeatinterval = int.Parse(txtRepeatInterval.Text);
            model.Settings.debug = cbDebugMode.Checked;
            model.Settings.osd = cbOSD.Checked;

            // OSD
            model.OSD.align = osdAlign.ToString();
            model.OSD.valign = osdVAlign.ToString();

            model.OSD.xysetting = cbXYSetting.Checked;
            model.OSD.pos_x = (int)nudX.Value;
            model.OSD.pos_y = (int)nudY.Value;

            model.OSD.fontFamily = osdTextFont.FontFamily.Name;
            model.OSD.fontSize = (int)osdTextFont.Size;
            model.OSD.fontStyle = osdTextFont.Style.ToString();
            model.OSD.textColor = ColorTranslator.ToHtml(osdTextColor);
            model.OSD.pathColor = ColorTranslator.ToHtml(osdPathColor);
            model.OSD.pathWidth = osdPathWidth;

            model.OSD.alpha = (byte)nudAlpha.Value;
            model.OSD.textTime = (int)nudTextTime.Value;
            model.OSD.animateEffect = cbAnimateEffect.SelectedItem.ToString();
            model.OSD.animateTime = (uint)nudAnimateTime.Value;
            model.OSD.testString = txtTestString.Text;

            model.OSD.osdWhen.appStart = cbOsdAppStart.Checked;
            model.OSD.osdWhen.remoteConnect = cbOsdRemoteConnect.Checked;
            model.OSD.osdWhen.remoteDisconnect = cbOsdRemoteDisconnect.Checked;
            model.OSD.osdWhen.remoteHibernate = cbOsdRemoteHibernate.Checked;
            model.OSD.osdWhen.remoteBatteryChange = cbOsdRemoteBatteryChange.Checked;

            model.OSD.osdWhen.remoteButtonPressed = cbOsdRemoteButtonPress.Checked;
            model.OSD.osdWhen.remoteButtonPressedAlways = rbOsdRemoteButtonPressAlways.Checked;
            model.OSD.osdWhen.remoteButtonPressedMatched = rbOsdRemoteButtonPressMatched.Checked;
            model.OSD.osdWhen.remoteButtonPressedAssigned = rbOsdRemoteButtonPressAssigned.Checked;

            model.OSD.osdWhen.activeWindowTitle = cbOsdActiveWindowTitle.Checked;
            model.OSD.osdWhen.mappingName = cbOsdMappingName.Checked;
            model.OSD.osdWhen.pressedRemoteButton = cbOsdPressedRemoteButton.Checked;
            model.OSD.osdWhen.assignedKey = cbOsdAssignedKey.Checked;

            model.Save();
        }

		private void SetLoggingEnabled(bool enable)
		{
			if (enable) {
				while (!LogManager.IsLoggingEnabled()) {
					LogManager.EnableLogging();
				}
			} else {
				while (LogManager.IsLoggingEnabled()) {
					LogManager.DisableLogging();
				}
			}
		}
        # endregion

        # region ### form events ###
        # region ## Mappings ##
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode App = model.Mappings[selectedIndex];

            MappingsForm mappingsForm = new MappingsForm(App);
            mappingsForm.ShowDialog(this);

            // save
            model.Mappings[selectedIndex] = mappingsForm.appMapping;

            mappingsForm.Dispose();
            lbAppsRedraw();
            lbApps.SelectedIndex = selectedIndex;
        }

        private void buttonUpper_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode selectedItem = model.Mappings[selectedIndex];
            model.Mappings.Remove(selectedItem);
            model.Mappings.Insert(selectedIndex - 1, selectedItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = selectedIndex - 1;
        }

        private void buttonLower_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode selectedItem = model.Mappings[selectedIndex];
            model.Mappings.Remove(selectedItem);
            model.Mappings.Insert(selectedIndex + 1, selectedItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = selectedIndex + 1;
        }

        private void buttonTop_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode selectedItem = model.Mappings[selectedIndex];
            model.Mappings.Remove(selectedItem);
            model.Mappings.Insert(0, selectedItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = 0;
        }

        private void buttonBottom_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode selectedItem = model.Mappings[selectedIndex];
            model.Mappings.Remove(selectedItem);
            model.Mappings.Add(selectedItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = lbApps.Items.Count - 1;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            AppNode newItem = new AppNode();
            newItem.name = "New";
            model.Mappings.Add(newItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = lbApps.Items.Count - 1;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;
            AppNode selectedItem = model.Mappings[selectedIndex];

            AppNode copiedItem = new AppNode();
            copiedItem.name = selectedItem.name + "_copy";
            copiedItem.buttonMappings = selectedItem.buttonMappings;
            copiedItem.caseSensitive = selectedItem.caseSensitive;
            copiedItem.condition = selectedItem.condition;
            model.Mappings.Add(copiedItem);

            lbAppsRedraw();
            lbApps.SelectedIndex = lbApps.Items.Count - 1;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbApps.SelectedIndex;

            AppNode selectedItem = model.Mappings[selectedIndex];
            model.Mappings.Remove(selectedItem);

            lbAppsRedraw();
            if (selectedIndex == 0)
                lbApps.SelectedIndex = 0;
            else
                lbApps.SelectedIndex = selectedIndex - 1;
        }

        private void lbApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbApps.Items.Count == 1)
            {
                buttonDelete.Enabled = false;
            }
            else
            {
                buttonDelete.Enabled = true;
            }

            if (lbApps.SelectedIndex == 0)
            {
                buttonTop.Enabled = false;
                buttonUpper.Enabled = false;
            }
            else
            {
                buttonTop.Enabled = true;
                buttonUpper.Enabled = true;
            }

            if (lbApps.SelectedIndex == lbApps.Items.Count - 1)
            {
                buttonBottom.Enabled = false;
                buttonLower.Enabled = false;
            }
            else
            {
                buttonBottom.Enabled = true;
                buttonLower.Enabled = true;
            }
        }

        private void lbAppsRedraw()
        {
            // lbApps re-draw
            // shoud use data bind and lbApps.refresh()
            while (lbApps.Items.Count > 0)
            {
                lbApps.Items.RemoveAt(0);
            }

            foreach (AppNode app in model.Mappings)
            {
                lbApps.Items.Add(app.name);
            }
        }
        # endregion
        
        # region ## Settings ##
        private void cbSms_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboard != null) keyboard.isSmsEnabled = cbSms.Checked;
        }

		private void cbStartup_CheckedChanged(object sender, EventArgs e)
		{
			var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			if (cbStartup.Checked) {
				key.SetValue("PS3BluMote", System.Reflection.Assembly.GetEntryAssembly().Location);
			} else if (key.GetValue("PS3BlueMote") != null) {
				key.DeleteValue("PS3BluMote");
			}
		}

		private void cbDebugMode_CheckedChanged(object sender, EventArgs e)
		{
			SetLoggingEnabled(cbDebugMode.Checked);
		}

        private void llblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = "explorer.exe";
            prc.StartInfo.Arguments = SETTINGS_DIRECTORY;
            prc.Start();
        }

		private void cbCustomRepeatRate_CheckedChanged(object sender, EventArgs e)
		{
			txtRepeatDelay.Enabled = cbCustomRepeatRate.Checked;
			txtRepeatInterval.Enabled = cbCustomRepeatRate.Checked;
		}
		
		private void txtRepeatDelay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try {
				int i = int.Parse(txtRepeatDelay.Text);
			} catch {
				e.Cancel = true;
			}
		}

		private void txtRepeatInterval_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int i = int.Parse(txtRepeatInterval.Text);
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txtProductId_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int i = int.Parse(txtProductId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txtVendorId_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int i = int.Parse(txtVendorId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                e.Cancel = true;
            }
        }
        # endregion

        # region ## OSD tab ##
        private void cbOSD_CheckedChanged(object sender, EventArgs e)
        {
            cbXYSetting.Enabled = cbOSD.Checked;
            rbLeft.Enabled = cbOSD.Checked && !cbXYSetting.Checked;
            rbCenter.Enabled = cbOSD.Checked && !cbXYSetting.Checked;
            rbRight.Enabled = cbOSD.Checked && !cbXYSetting.Checked;

            rbTop.Enabled = cbOSD.Checked && !cbXYSetting.Checked;
            rbMiddle.Enabled = cbOSD.Checked && !cbXYSetting.Checked;
            rbBottom.Enabled = cbOSD.Checked && !cbXYSetting.Checked;

            nudX.Enabled = cbOSD.Checked && cbXYSetting.Checked;
            nudY.Enabled = cbOSD.Checked && cbXYSetting.Checked;

            buttonFontPick.Enabled = cbOSD.Checked;
            buttonTextColorPick.Enabled = cbOSD.Checked;
            buttonPathColorPick.Enabled = cbOSD.Checked;

            nudAlpha.Enabled = cbOSD.Checked;
            nudTextTime.Enabled = cbOSD.Checked;
            cbAnimateEffect.Enabled = cbOSD.Checked;
            nudAnimateTime.Enabled = cbOSD.Checked;

            cbOsdAppStart.Enabled = cbOSD.Checked;
            cbOsdRemoteConnect.Enabled = cbOSD.Checked;
            cbOsdRemoteDisconnect.Enabled = cbOSD.Checked;
            cbOsdRemoteHibernate.Enabled = cbOSD.Checked;
            cbOsdRemoteBatteryChange.Enabled = cbOSD.Checked;

            cbOsdRemoteButtonPress.Enabled = cbOSD.Checked;
            rbOsdRemoteButtonPressAlways.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressMatched.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressAssigned.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;

            cbOsdActiveWindowTitle.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdMappingName.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdPressedRemoteButton.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;
            cbOsdAssignedKey.Enabled = cbOSD.Checked && cbOsdRemoteButtonPress.Checked;

            txtTestString.Enabled = cbOSD.Checked;
            buttonTestOsd.Enabled = cbOSD.Checked;

            // ---
            if (cbOSD.Checked == true)
            {
                osd = new OsdWindow();
            }
            else
            {
                osd.Close();
                GC.Collect();
            }
        }

        private void rbAlign_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLeft.Checked == true)
                osdAlign = OsdWindow.OsdTextAlign.Left;
            else if (rbRight.Checked == true)
                osdAlign = OsdWindow.OsdTextAlign.Right;
            else
                osdAlign = OsdWindow.OsdTextAlign.Center;
        }

        private void rbVerticalAlign_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTop.Checked == true)
                osdVAlign = OsdWindow.OsdVerticalAlign.Top;
            else if (rbMiddle.Checked == true)
                osdVAlign = OsdWindow.OsdVerticalAlign.Middle;
            else
                osdVAlign = OsdWindow.OsdVerticalAlign.Bottom;
        }

        private void cbXYSetting_CheckedChanged(object sender, EventArgs e)
        {
            rbLeft.Enabled = !cbXYSetting.Checked;
            rbCenter.Enabled = !cbXYSetting.Checked;
            rbRight.Enabled = !cbXYSetting.Checked;

            rbTop.Enabled = !cbXYSetting.Checked;
            rbMiddle.Enabled = !cbXYSetting.Checked;
            rbBottom.Enabled = !cbXYSetting.Checked;

            nudX.Enabled = cbXYSetting.Checked;
            nudY.Enabled = cbXYSetting.Checked;
        }

        private void buttonFontPick_Click(object sender, EventArgs e)
        {
            dialogFont.Font = osdTextFont;
            if (dialogFont.ShowDialog() == DialogResult.OK)
                osdTextFont = dialogFont.Font;
        }

        private void buttonTextColorPick_Click(object sender, EventArgs e)
        {
            dialogTextColor.Color = osdTextColor;
            if (dialogTextColor.ShowDialog() == DialogResult.OK)
                osdTextColor = dialogTextColor.Color;
        }

        private void buttonPathColorPick_Click(object sender, EventArgs e)
        {
            dialogPathColor.Color = osdPathColor;
            if (dialogPathColor.ShowDialog() == DialogResult.OK)
                osdPathColor = dialogPathColor.Color;
        }

        private void cbOsdRemoteButtonPress_CheckedChanged(object sender, EventArgs e)
        {
            rbOsdRemoteButtonPressAlways.Enabled = cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressMatched.Enabled = cbOsdRemoteButtonPress.Checked;
            rbOsdRemoteButtonPressAssigned.Enabled = cbOsdRemoteButtonPress.Checked;

            cbOsdActiveWindowTitle.Enabled = cbOsdRemoteButtonPress.Checked;
            cbOsdMappingName.Enabled = cbOsdRemoteButtonPress.Checked;
            cbOsdPressedRemoteButton.Enabled = cbOsdRemoteButtonPress.Checked;
            cbOsdAssignedKey.Enabled = cbOsdRemoteButtonPress.Checked;
        }

        private void buttonTestOsd_Click(object sender, EventArgs e)
        {
            ShowOsd(txtTestString.Text);
        }
        # endregion

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
	        if (Visible) {
		        Hide();
	        } else {
		        Show();
	        }
        }

        private void menuNotifyIcon_ItemClick(object sender, EventArgs e)
        {
            if (sender.Equals(mitemSettings))
            {
                Show();
            }
            else if (sender.Equals(mitemExit))
            {
                Application.Exit();
            }
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            lbApps.Focus();
            lbApps.SelectedIndex = 0;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();

            if (e.CloseReason != CloseReason.UserClosing) return;

            e.Cancel = true;

            Hide();
        }
        # endregion

        # region ### remote ###
        private void remoteService_OnButtonDown(object sender, ButtonData e)
        {
            Log.Debug("Button down: " + e.Button.ToString());

            ButtonMapping mapping = null;

            var showString = new StringBuilder("Remote Button is Pressed");
            string activeWindowTitle = GetActiveWindowTitle();
            if (activeWindowTitle == null) activeWindowTitle = "";
            if (cbOsdActiveWindowTitle.Checked && activeWindowTitle != "") showString.Append("\n" + activeWindowTitle);

            foreach (AppNode app in model.Mappings) {
                bool ignoreCase = !app.caseSensitive;
                if (Regex.IsMatch(activeWindowTitle, app.condition, (RegexOptions)(ignoreCase ? 1 : 0))) {
                    mapping = app.buttonMappings[(int)e.Button];
                    Log.Debug("Matched: {" + activeWindowTitle + "} {" + app.name + "}");
                    if (cbOsdMappingName.Checked && app.name != "")
                        if (activeWindowTitle != "")
                            showString.Append(": " + app.name);
                        else
                            showString.Append("\n" + app.name);
                    break;
                }
            }
            if (cbOsdPressedRemoteButton.Checked)
                showString.Append("\n" + e.Button.ToString());

            if (mapping == null) {
                Log.Debug("Keys down: { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");
                Log.Debug("Keys unmatch: Active App Window Title {" + activeWindowTitle + "}");
            } else {
                if (cbOsdAssignedKey.Checked)
                    if (e.Button.ToString() != "")
                        showString.Append(": " + mapping.joinedKeyMapped.Replace(",", " + "));
                    else
                        showString.Append("\n" + mapping.joinedKeyMapped.Replace(",", " + "));

                if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressMatched.Checked)
                    ShowOsd(showString.ToString());
                if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressAssigned.Checked && mapping.keysMapped != null)
                    ShowOsd(showString.ToString());

                if (mapping.repeat) {
                    keyboard.sendKeysDown(mapping.keysMapped);
                    keyboard.releaseLastKeys();
                    Log.Debug("Keys repeat send on : { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");

	                if (!model.Settings.usecustomdelay) {
		                timerRepeat.Change(KeyboardDelay, KeyboardRepeat);
	                } else {
						timerRepeat.Change(model.Settings.repeatdelay, model.Settings.repeatinterval);
					}
	                repeating = true;
                } else {
                    keyboard.sendKeysDown(mapping.keysMapped);
                    Log.Debug("Keys down: { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");
                }
            }

            if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressAlways.Checked)
                ShowOsd(showString.ToString());
        }

        private void remoteService_OnButtonReleased(object sender, EventArgs e)
        {
            Log.Debug("Button released");

            if (repeating) {
                Log.Debug("Keys repeat send off: { " + String.Join(",", keyboard.lastKeysDown.ToArray()) + " }");
	            timerRepeat.Change(Timeout.Infinite, Timeout.Infinite);
                repeating = false;
                return;
            }

            if (keyboard.lastKeysDown != null) Log.Debug("Keys up: { " + String.Join(",", keyboard.lastKeysDown.ToArray()) + " }");

            keyboard.releaseLastKeys();
        }

		private void remoteService_OnDisconnected(object sender, RemoteStateData e)
		{
			Log.Debug("Remote disconnected");
			if (cbOsdRemoteDisconnect.Checked) ShowOsd("Remote disconnected");

			notifyIcon.Text = "PS3BluMote: Disconnected.";
			notifyIcon.Icon = Properties.Resources.Icon_Disconnected;
		}

		private void remoteService_OnConnected(object sender, RemoteStateData e)
		{
			Log.Debug("Remote connected");
			if (cbOsdRemoteConnect.Checked) {
				ShowOsd("Remote connected");
			}

			notifyIcon.Text = "PS3BluMote: Connected (Battery: " + e.BatteryLife + ").";
			notifyIcon.Icon = Properties.Resources.Icon_Connected;
		}

		private void remoteService_OnBatteryLifeChanged(object sender, RemoteStateData e)
		{
			notifyIcon.Text = "PS3BluMote: Connected + (Battery: " + e.BatteryLife + ").";

			Log.Debug("Battery life: " + e.BatteryLife);
			if (cbOsdRemoteBatteryChange.Checked) ShowOsd("Battery life: " + e.BatteryLife);
		}
        # endregion

        # region ### OSD ###
        delegate void ShowOSDCallback(string text);
        void ShowOsd(string text)
        {
            if (!cbOSD.Checked) return;
            if (cbXYSetting.Checked == true)
            {
                osd.Show(
                    new Point(int.Parse(nudX.Value.ToString()), int.Parse(nudY.Value.ToString())),
                    byte.Parse(nudAlpha.Value.ToString()), osdTextColor, osdPathColor, osdPathWidth,
                    osdTextFont, int.Parse(nudTextTime.Value.ToString()),
                    (OsdWindow.AnimateMode)Enum.Parse(typeof(OsdWindow.AnimateMode), cbAnimateEffect.SelectedItem.ToString()),
                    (uint)nudAnimateTime.Value, text
                );
            }
            else
            {
                if (this.cbAnimateEffect.InvokeRequired)
                {
                    ShowOSDCallback d = new ShowOSDCallback(ShowOsd);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    osd.Show(
                        osdAlign, osdVAlign,
                        byte.Parse(nudAlpha.Value.ToString()), osdTextColor, osdPathColor, osdPathWidth,
                        osdTextFont, int.Parse(nudTextTime.Value.ToString()),
                        (OsdWindow.AnimateMode)Enum.Parse(typeof(OsdWindow.AnimateMode), cbAnimateEffect.SelectedItem.ToString()),
                        (uint)nudAnimateTime.Value, text
                    );
                }
            }
        }
        # endregion

        # region ### get active window title ###
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        # endregion

		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    }
}

