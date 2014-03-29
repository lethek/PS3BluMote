/*
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
using System.Collections.Generic;
using System.Windows.Forms;

using WindowsAPI;

// OSD
using System.Drawing;
using System.Text;

// get active window title (DllImport)
using System.Runtime.InteropServices;

using System.Text.RegularExpressions;
using NLog;

namespace PS3BluMote
{
    public partial class SettingsForm : Form
    {
        # region ### fields ###
        private readonly String SETTINGS_DIRECTORY = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\PS3BluMote\\";
        private readonly String SETTINGS_FILE = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\PS3BluMote\\settings.ini";

        private PS3Remote remote;
        private SendInputAPI.Keyboard keyboard;
        private System.Timers.Timer timerRepeat;

        public ModelXml model;

        private OsdWindow osd;
        private OsdWindow.OsdTextAlign osdAlign;
        private OsdWindow.OsdVerticalAlign osdVAlign;
        private Font osdTextFont;
        private Color osdTextColor;
        private Color osdPathColor;
        private Single osdPathWidth;
        private Rectangle rScreen = Screen.PrimaryScreen.Bounds;
        # endregion 

        public SettingsForm()
        {
            InitializeComponent();

            model = new ModelXml(SETTINGS_FILE);
            SetForms();
            keyboard = new SendInputAPI.Keyboard(cbSms.Checked);

            timerRepeat = new System.Timers.Timer();
            timerRepeat.Interval = model.Settings.repeatinterval;
            timerRepeat.Elapsed += new System.Timers.ElapsedEventHandler(timerRepeat_Elapsed);

            SetRemote();

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

            txtRepeatInterval.Text = model.Settings.repeatinterval.ToString();

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

        private void timerRepeat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            keyboard.sendKeysDown(keyboard.lastKeysDown);
            keyboard.releaseLastKeys();
        }

        private void SetRemote()
        {
            try
            {
                remote = new PS3Remote(int.Parse(txtVendorId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber), int.Parse(txtProductId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber), false);

                remote.BatteryLifeChanged += new EventHandler<EventArgs>(remote_BatteryLifeChanged);
                remote.ButtonDown += new EventHandler<PS3Remote.ButtonData>(remote_ButtonDown);
                remote.ButtonReleased += new EventHandler<PS3Remote.ButtonData>(remote_ButtonReleased);
                remote.Connected += new EventHandler<EventArgs>(remote_Connected);
                remote.Disconnected += new EventHandler<EventArgs>(remote_Disconnected);

                remote.connect();
            }
            catch
            {
                MessageBox.Show(
                    "An error occured whilst attempting to load the remote.",
                    "PS3BluMote: Remote error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void saveSettings()
        {
            // should use data bind?
            model.Settings.vendorid = txtVendorId.Text.ToLower();
            model.Settings.productid = txtProductId.Text.ToLower();
            model.Settings.smsinput = cbSms.Checked;
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

        private void llblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = "explorer.exe";
            prc.StartInfo.Arguments = SETTINGS_DIRECTORY;
            prc.Start();
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
            Show();
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
        private void remote_ButtonDown(object sender, PS3Remote.ButtonData e)
        {
            Log.Debug("Button down: " + e.button.ToString());

            ButtonMapping mapping = null;

            StringBuilder showString = new StringBuilder("Remote Button is Pressed");
            string activeWindowTitle = GetActiveWindowTitle();
            if (activeWindowTitle == null) activeWindowTitle = "";
            if (cbOsdActiveWindowTitle.Checked && activeWindowTitle != "") showString.Append("\n" + activeWindowTitle);

            foreach (AppNode app in model.Mappings)
            {
                bool ignoreCase = !app.caseSensitive;
                if (Regex.IsMatch(activeWindowTitle, app.condition, (RegexOptions)(ignoreCase ? 1 : 0)))
                {
                    mapping = app.buttonMappings[(int)e.button];
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
                showString.Append("\n" + e.button.ToString());

            if (mapping == null)
            {
                Log.Debug("Keys down: { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");
                Log.Debug("Keys unmatch: Active App Window Title {" + activeWindowTitle + "}");
            }
            else
            {
                if (cbOsdAssignedKey.Checked)
                    if (e.button.ToString() != "")
                        showString.Append(": " + mapping.joinedKeyMapped.Replace(",", " + "));
                    else
                        showString.Append("\n" + mapping.joinedKeyMapped.Replace(",", " + "));

                if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressMatched.Checked)
                    ShowOsd(showString.ToString());
                if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressAssigned.Checked && mapping.keysMapped != null)
                    ShowOsd(showString.ToString());

                if (mapping.repeat)
                {
                    keyboard.sendKeysDown(mapping.keysMapped);
                    keyboard.releaseLastKeys();
                    Log.Debug("Keys repeat send on : { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");

                    timerRepeat.Enabled = true;
                }
                else
                {
                    keyboard.sendKeysDown(mapping.keysMapped);
                    Log.Debug("Keys down: { " + String.Join(",", mapping.keysMapped.ToArray()) + " }");
                }
            }

            if (cbOsdRemoteButtonPress.Checked && rbOsdRemoteButtonPressAlways.Checked)
                ShowOsd(showString.ToString());
        }

        private void remote_ButtonReleased(object sender, PS3Remote.ButtonData e)
        {
            Log.Debug("Button released: " + e.button.ToString());

            if (timerRepeat.Enabled)
            {
                Log.Debug("Keys repeat send off: { " + String.Join(",", keyboard.lastKeysDown.ToArray()) + " }");

                timerRepeat.Enabled = false;
                return;
            }

            if (keyboard.lastKeysDown != null) Log.Debug("Keys up: { " + String.Join(",", keyboard.lastKeysDown.ToArray()) + " }");

            keyboard.releaseLastKeys();
        }

        private void remote_Connected(object sender, EventArgs e)
        {
            Log.Debug("Remote connected");
            if (cbOsdRemoteConnect.Checked) ShowOsd("Remote connected");

            notifyIcon.Text = "PS3BluMote: Connected (Battery: " + remote.getBatteryLifeString() + ").";
            notifyIcon.Icon = Properties.Resources.Icon_Connected;
        }

        private void remote_Disconnected(object sender, EventArgs e)
        {
            Log.Debug("Remote disconnected");
            if (cbOsdRemoteDisconnect.Checked) ShowOsd("Remote disconnected");

            notifyIcon.Text = "PS3BluMote: Disconnected.";
            notifyIcon.Icon = Properties.Resources.Icon_Disconnected;
        }

        private void remote_BatteryLifeChanged(object sender, EventArgs e)
        {
            notifyIcon.Text = "PS3BluMote: Connected + (Battery: " + remote.getBatteryLife.ToString() + "%).";

            Log.Debug("Battery life: " + remote.getBatteryLife.ToString() + "%");
            if (cbOsdRemoteBatteryChange.Checked) ShowOsd("Battery life: " + remote.getBatteryLife.ToString() + "%");
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

