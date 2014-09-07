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
*/

namespace PS3BluMote
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.menuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mitemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.mitemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabMappings = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.txtRepeatDelay = new System.Windows.Forms.TextBox();
			this.lblRepeatDelay = new System.Windows.Forms.Label();
			this.cbCustomRepeatRate = new System.Windows.Forms.CheckBox();
			this.txtRepeatInterval = new System.Windows.Forms.TextBox();
			this.lblRepeatInterval = new System.Windows.Forms.Label();
			this.cbStartup = new System.Windows.Forms.CheckBox();
			this.llblOpenFolder = new System.Windows.Forms.LinkLabel();
			this.cbDebugMode = new System.Windows.Forms.CheckBox();
			this.gbAdvanced = new System.Windows.Forms.GroupBox();
			this.lblRemoteCodes = new System.Windows.Forms.Label();
			this.txtVendorId = new System.Windows.Forms.TextBox();
			this.txtProductId = new System.Windows.Forms.TextBox();
			this.lblVendorId = new System.Windows.Forms.Label();
			this.lblProductId = new System.Windows.Forms.Label();
			this.cbSms = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.buttonBottom = new System.Windows.Forms.Button();
			this.lbApps = new System.Windows.Forms.ListBox();
			this.buttonUpper = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonNew = new System.Windows.Forms.Button();
			this.buttonTop = new System.Windows.Forms.Button();
			this.buttonLower = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.tabOSD1 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTestString = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cbOsdRemoteButtonPress = new System.Windows.Forms.CheckBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.rbOsdRemoteButtonPressMatched = new System.Windows.Forms.RadioButton();
			this.rbOsdRemoteButtonPressAssigned = new System.Windows.Forms.RadioButton();
			this.rbOsdRemoteButtonPressAlways = new System.Windows.Forms.RadioButton();
			this.cbOsdAssignedKey = new System.Windows.Forms.CheckBox();
			this.cbOsdActiveWindowTitle = new System.Windows.Forms.CheckBox();
			this.cbOsdPressedRemoteButton = new System.Windows.Forms.CheckBox();
			this.cbOsdMappingName = new System.Windows.Forms.CheckBox();
			this.cbOsdRemoteBatteryChange = new System.Windows.Forms.CheckBox();
			this.cbOsdRemoteHibernate = new System.Windows.Forms.CheckBox();
			this.cbOsdRemoteDisconnect = new System.Windows.Forms.CheckBox();
			this.cbOsdRemoteConnect = new System.Windows.Forms.CheckBox();
			this.cbOsdAppStart = new System.Windows.Forms.CheckBox();
			this.buttonTestOsd = new System.Windows.Forms.Button();
			this.cbOSD = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbAnimateEffect = new System.Windows.Forms.ComboBox();
			this.lblAnimateEffect = new System.Windows.Forms.Label();
			this.nudAnimateTime = new System.Windows.Forms.NumericUpDown();
			this.lblAnimateTimeDescription = new System.Windows.Forms.Label();
			this.lblAnimateTime = new System.Windows.Forms.Label();
			this.buttonFontPick = new System.Windows.Forms.Button();
			this.buttonPathColorPick = new System.Windows.Forms.Button();
			this.buttonTextColorPick = new System.Windows.Forms.Button();
			this.nudAlpha = new System.Windows.Forms.NumericUpDown();
			this.lblAlpha = new System.Windows.Forms.Label();
			this.lblTextTime = new System.Windows.Forms.Label();
			this.nudTextTime = new System.Windows.Forms.NumericUpDown();
			this.gbXYPositionOfTopLeftCorner = new System.Windows.Forms.GroupBox();
			this.cbXYSetting = new System.Windows.Forms.CheckBox();
			this.gbVerticalAlign = new System.Windows.Forms.GroupBox();
			this.rbBottom = new System.Windows.Forms.RadioButton();
			this.rbMiddle = new System.Windows.Forms.RadioButton();
			this.rbTop = new System.Windows.Forms.RadioButton();
			this.gbAlign = new System.Windows.Forms.GroupBox();
			this.rbRight = new System.Windows.Forms.RadioButton();
			this.rbCenter = new System.Windows.Forms.RadioButton();
			this.rbLeft = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.nudX = new System.Windows.Forms.NumericUpDown();
			this.nudY = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.toolTipAdvanced = new System.Windows.Forms.ToolTip(this.components);
			this.dialogTextColor = new System.Windows.Forms.ColorDialog();
			this.dialogPathColor = new System.Windows.Forms.ColorDialog();
			this.dialogFont = new System.Windows.Forms.FontDialog();
			this.menuNotifyIcon.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.tabMappings.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.gbAdvanced.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tabOSD1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAnimateTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTextTime)).BeginInit();
			this.gbXYPositionOfTopLeftCorner.SuspendLayout();
			this.gbVerticalAlign.SuspendLayout();
			this.gbAlign.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.menuNotifyIcon;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "PS3BluMote: Disconnected.";
			this.notifyIcon.Visible = true;
			this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
			// 
			// menuNotifyIcon
			// 
			this.menuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mitemSettings,
            this.mitemExit});
			this.menuNotifyIcon.Name = "menuNotifyIcon";
			this.menuNotifyIcon.Size = new System.Drawing.Size(117, 48);
			// 
			// mitemSettings
			// 
			this.mitemSettings.Name = "mitemSettings";
			this.mitemSettings.Size = new System.Drawing.Size(116, 22);
			this.mitemSettings.Text = "Settings";
			this.mitemSettings.Click += new System.EventHandler(this.menuNotifyIcon_ItemClick);
			// 
			// mitemExit
			// 
			this.mitemExit.Name = "mitemExit";
			this.mitemExit.Size = new System.Drawing.Size(116, 22);
			this.mitemExit.Text = "Exit";
			this.mitemExit.Click += new System.EventHandler(this.menuNotifyIcon_ItemClick);
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.tabMappings);
			this.TabControl.Controls.Add(this.tabOSD1);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(6, 7);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(579, 464);
			this.TabControl.TabIndex = 1;
			// 
			// tabMappings
			// 
			this.tabMappings.Controls.Add(this.groupBox5);
			this.tabMappings.Controls.Add(this.groupBox4);
			this.tabMappings.Controls.Add(this.lblCopyright);
			this.tabMappings.Location = new System.Drawing.Point(4, 22);
			this.tabMappings.Name = "tabMappings";
			this.tabMappings.Padding = new System.Windows.Forms.Padding(3);
			this.tabMappings.Size = new System.Drawing.Size(571, 438);
			this.tabMappings.TabIndex = 0;
			this.tabMappings.Text = "Settings";
			this.tabMappings.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.groupBox7);
			this.groupBox5.Controls.Add(this.cbStartup);
			this.groupBox5.Controls.Add(this.llblOpenFolder);
			this.groupBox5.Controls.Add(this.cbDebugMode);
			this.groupBox5.Controls.Add(this.gbAdvanced);
			this.groupBox5.Controls.Add(this.cbSms);
			this.groupBox5.Location = new System.Drawing.Point(293, 7);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(272, 373);
			this.groupBox5.TabIndex = 10;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Settings";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.txtRepeatDelay);
			this.groupBox7.Controls.Add(this.lblRepeatDelay);
			this.groupBox7.Controls.Add(this.cbCustomRepeatRate);
			this.groupBox7.Controls.Add(this.txtRepeatInterval);
			this.groupBox7.Controls.Add(this.lblRepeatInterval);
			this.groupBox7.Location = new System.Drawing.Point(6, 94);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(260, 97);
			this.groupBox7.TabIndex = 24;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Repeat Rate";
			// 
			// txtRepeatDelay
			// 
			this.txtRepeatDelay.Location = new System.Drawing.Point(163, 44);
			this.txtRepeatDelay.Name = "txtRepeatDelay";
			this.txtRepeatDelay.Size = new System.Drawing.Size(46, 20);
			this.txtRepeatDelay.TabIndex = 28;
			this.txtRepeatDelay.Text = "500";
			this.txtRepeatDelay.Validating += new System.ComponentModel.CancelEventHandler(this.txtRepeatDelay_Validating);
			// 
			// lblRepeatDelay
			// 
			this.lblRepeatDelay.AutoSize = true;
			this.lblRepeatDelay.Location = new System.Drawing.Point(12, 47);
			this.lblRepeatDelay.Name = "lblRepeatDelay";
			this.lblRepeatDelay.Size = new System.Drawing.Size(132, 13);
			this.lblRepeatDelay.TabIndex = 27;
			this.lblRepeatDelay.Text = "Button repeat delay (in ms)";
			// 
			// cbCustomRepeatRate
			// 
			this.cbCustomRepeatRate.AutoSize = true;
			this.cbCustomRepeatRate.Location = new System.Drawing.Point(12, 19);
			this.cbCustomRepeatRate.Name = "cbCustomRepeatRate";
			this.cbCustomRepeatRate.Size = new System.Drawing.Size(136, 17);
			this.cbCustomRepeatRate.TabIndex = 26;
			this.cbCustomRepeatRate.Text = "Use custom repeat rate";
			this.toolTipAdvanced.SetToolTip(this.cbCustomRepeatRate, "When disabled, the system\'s keyboard repeat rate and delay are used instead.");
			this.cbCustomRepeatRate.UseVisualStyleBackColor = true;
			this.cbCustomRepeatRate.CheckedChanged += new System.EventHandler(this.cbCustomRepeatRate_CheckedChanged);
			// 
			// txtRepeatInterval
			// 
			this.txtRepeatInterval.Location = new System.Drawing.Point(163, 70);
			this.txtRepeatInterval.Name = "txtRepeatInterval";
			this.txtRepeatInterval.Size = new System.Drawing.Size(46, 20);
			this.txtRepeatInterval.TabIndex = 25;
			this.txtRepeatInterval.Text = "250";
			this.txtRepeatInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtRepeatInterval_Validating);
			// 
			// lblRepeatInterval
			// 
			this.lblRepeatInterval.AutoSize = true;
			this.lblRepeatInterval.Location = new System.Drawing.Point(12, 73);
			this.lblRepeatInterval.Name = "lblRepeatInterval";
			this.lblRepeatInterval.Size = new System.Drawing.Size(144, 13);
			this.lblRepeatInterval.TabIndex = 24;
			this.lblRepeatInterval.Text = "Button repeat interval (in ms):";
			// 
			// cbStartup
			// 
			this.cbStartup.AutoSize = true;
			this.cbStartup.Location = new System.Drawing.Point(6, 45);
			this.cbStartup.Name = "cbStartup";
			this.cbStartup.Size = new System.Drawing.Size(95, 17);
			this.cbStartup.TabIndex = 8;
			this.cbStartup.Text = "Run at Startup";
			this.cbStartup.UseVisualStyleBackColor = true;
			this.cbStartup.CheckedChanged += new System.EventHandler(this.cbStartup_CheckedChanged);
			// 
			// llblOpenFolder
			// 
			this.llblOpenFolder.AutoSize = true;
			this.llblOpenFolder.Location = new System.Drawing.Point(187, 72);
			this.llblOpenFolder.Name = "llblOpenFolder";
			this.llblOpenFolder.Size = new System.Drawing.Size(79, 13);
			this.llblOpenFolder.TabIndex = 17;
			this.llblOpenFolder.TabStop = true;
			this.llblOpenFolder.Text = "Open log folder";
			this.llblOpenFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.llblOpenFolder.VisitedLinkColor = System.Drawing.Color.Blue;
			this.llblOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblOpenFolder_LinkClicked);
			// 
			// cbDebugMode
			// 
			this.cbDebugMode.AutoSize = true;
			this.cbDebugMode.Location = new System.Drawing.Point(6, 71);
			this.cbDebugMode.Name = "cbDebugMode";
			this.cbDebugMode.Size = new System.Drawing.Size(171, 17);
			this.cbDebugMode.TabIndex = 16;
			this.cbDebugMode.Text = "Debug mode (logging enabled)";
			this.cbDebugMode.UseVisualStyleBackColor = true;
			// 
			// gbAdvanced
			// 
			this.gbAdvanced.Controls.Add(this.lblRemoteCodes);
			this.gbAdvanced.Controls.Add(this.txtVendorId);
			this.gbAdvanced.Controls.Add(this.txtProductId);
			this.gbAdvanced.Controls.Add(this.lblVendorId);
			this.gbAdvanced.Controls.Add(this.lblProductId);
			this.gbAdvanced.Location = new System.Drawing.Point(6, 193);
			this.gbAdvanced.Name = "gbAdvanced";
			this.gbAdvanced.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.gbAdvanced.Size = new System.Drawing.Size(260, 169);
			this.gbAdvanced.TabIndex = 15;
			this.gbAdvanced.TabStop = false;
			this.gbAdvanced.Text = "Advanced";
			this.toolTipAdvanced.SetToolTip(this.gbAdvanced, "For when using a remote OTHER THAN the official Sony PS3 Remote.");
			// 
			// lblRemoteCodes
			// 
			this.lblRemoteCodes.AutoSize = true;
			this.lblRemoteCodes.Location = new System.Drawing.Point(22, 80);
			this.lblRemoteCodes.Name = "lblRemoteCodes";
			this.lblRemoteCodes.Size = new System.Drawing.Size(187, 78);
			this.lblRemoteCodes.TabIndex = 3;
			this.lblRemoteCodes.Text = "Official Sony PS3 Blutetooth remote:\r\n    PID: 0x0306    VID: 0x054c\r\nSMK Blu-Lin" +
    "k VP3700:\r\n    PID: 0x0306    VID: 0x0609\r\nLogitech Harmony (with PS3 Adapter):\r" +
    "\n    PID: 0xc129    VID: 0x046d";
			// 
			// txtVendorId
			// 
			this.txtVendorId.Location = new System.Drawing.Point(90, 51);
			this.txtVendorId.Name = "txtVendorId";
			this.txtVendorId.Size = new System.Drawing.Size(119, 20);
			this.txtVendorId.TabIndex = 3;
			this.txtVendorId.Text = "0x054c";
			this.txtVendorId.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorId_Validating);
			// 
			// txtProductId
			// 
			this.txtProductId.Location = new System.Drawing.Point(90, 22);
			this.txtProductId.Name = "txtProductId";
			this.txtProductId.Size = new System.Drawing.Size(119, 20);
			this.txtProductId.TabIndex = 2;
			this.txtProductId.Text = "0x0306";
			this.txtProductId.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductId_Validating);
			// 
			// lblVendorId
			// 
			this.lblVendorId.AutoSize = true;
			this.lblVendorId.Location = new System.Drawing.Point(9, 54);
			this.lblVendorId.Name = "lblVendorId";
			this.lblVendorId.Size = new System.Drawing.Size(58, 13);
			this.lblVendorId.TabIndex = 1;
			this.lblVendorId.Text = "Vendor ID:";
			// 
			// lblProductId
			// 
			this.lblProductId.AutoSize = true;
			this.lblProductId.Location = new System.Drawing.Point(9, 25);
			this.lblProductId.Name = "lblProductId";
			this.lblProductId.Size = new System.Drawing.Size(61, 13);
			this.lblProductId.TabIndex = 0;
			this.lblProductId.Text = "Product ID:";
			// 
			// cbSms
			// 
			this.cbSms.AutoSize = true;
			this.cbSms.Location = new System.Drawing.Point(6, 20);
			this.cbSms.Name = "cbSms";
			this.cbSms.Size = new System.Drawing.Size(95, 17);
			this.cbSms.TabIndex = 13;
			this.cbSms.Text = "SMS text input";
			this.cbSms.UseVisualStyleBackColor = true;
			this.cbSms.CheckedChanged += new System.EventHandler(this.cbSms_CheckedChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.buttonBottom);
			this.groupBox4.Controls.Add(this.lbApps);
			this.groupBox4.Controls.Add(this.buttonUpper);
			this.groupBox4.Controls.Add(this.buttonEdit);
			this.groupBox4.Controls.Add(this.buttonNew);
			this.groupBox4.Controls.Add(this.buttonTop);
			this.groupBox4.Controls.Add(this.buttonLower);
			this.groupBox4.Controls.Add(this.buttonDelete);
			this.groupBox4.Controls.Add(this.buttonCopy);
			this.groupBox4.Location = new System.Drawing.Point(6, 7);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(266, 373);
			this.groupBox4.TabIndex = 9;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Mappings";
			// 
			// buttonBottom
			// 
			this.buttonBottom.Location = new System.Drawing.Point(185, 163);
			this.buttonBottom.Name = "buttonBottom";
			this.buttonBottom.Size = new System.Drawing.Size(75, 25);
			this.buttonBottom.TabIndex = 8;
			this.buttonBottom.Text = "ToBottom";
			this.buttonBottom.UseVisualStyleBackColor = true;
			this.buttonBottom.Click += new System.EventHandler(this.buttonBottom_Click);
			// 
			// lbApps
			// 
			this.lbApps.FormattingEnabled = true;
			this.lbApps.Location = new System.Drawing.Point(6, 20);
			this.lbApps.Name = "lbApps";
			this.lbApps.Size = new System.Drawing.Size(173, 342);
			this.lbApps.TabIndex = 0;
			this.lbApps.SelectedIndexChanged += new System.EventHandler(this.lbApps_SelectedIndexChanged);
			// 
			// buttonUpper
			// 
			this.buttonUpper.Location = new System.Drawing.Point(185, 60);
			this.buttonUpper.Name = "buttonUpper";
			this.buttonUpper.Size = new System.Drawing.Size(75, 25);
			this.buttonUpper.TabIndex = 1;
			this.buttonUpper.Text = "Upper";
			this.buttonUpper.UseVisualStyleBackColor = true;
			this.buttonUpper.Click += new System.EventHandler(this.buttonUpper_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Location = new System.Drawing.Point(185, 20);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(75, 25);
			this.buttonEdit.TabIndex = 3;
			this.buttonEdit.Text = "Edit";
			this.buttonEdit.UseVisualStyleBackColor = true;
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonNew
			// 
			this.buttonNew.Location = new System.Drawing.Point(185, 203);
			this.buttonNew.Name = "buttonNew";
			this.buttonNew.Size = new System.Drawing.Size(75, 25);
			this.buttonNew.TabIndex = 4;
			this.buttonNew.Text = "New";
			this.buttonNew.UseVisualStyleBackColor = true;
			this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
			// 
			// buttonTop
			// 
			this.buttonTop.Location = new System.Drawing.Point(185, 131);
			this.buttonTop.Name = "buttonTop";
			this.buttonTop.Size = new System.Drawing.Size(75, 25);
			this.buttonTop.TabIndex = 7;
			this.buttonTop.Text = "ToTop";
			this.buttonTop.UseVisualStyleBackColor = true;
			this.buttonTop.Click += new System.EventHandler(this.buttonTop_Click);
			// 
			// buttonLower
			// 
			this.buttonLower.Location = new System.Drawing.Point(185, 91);
			this.buttonLower.Name = "buttonLower";
			this.buttonLower.Size = new System.Drawing.Size(75, 25);
			this.buttonLower.TabIndex = 2;
			this.buttonLower.Text = "Lower";
			this.buttonLower.UseVisualStyleBackColor = true;
			this.buttonLower.Click += new System.EventHandler(this.buttonLower_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(185, 337);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 25);
			this.buttonDelete.TabIndex = 6;
			this.buttonDelete.Text = "Delete";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonCopy
			// 
			this.buttonCopy.Location = new System.Drawing.Point(185, 234);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(75, 25);
			this.buttonCopy.TabIndex = 5;
			this.buttonCopy.Text = "Copy";
			this.buttonCopy.UseVisualStyleBackColor = true;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// lblCopyright
			// 
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(3, 383);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(160, 52);
			this.lblCopyright.TabIndex = 18;
			this.lblCopyright.Text = "PS3BluMote v2.2.0.\r\nCopyright © Ben Barron 2012.\r\nHibernation by Miljbee\r\nmulti m" +
    "appings by gridgem 2014.";
			// 
			// tabOSD1
			// 
			this.tabOSD1.Controls.Add(this.label3);
			this.tabOSD1.Controls.Add(this.txtTestString);
			this.tabOSD1.Controls.Add(this.groupBox3);
			this.tabOSD1.Controls.Add(this.buttonTestOsd);
			this.tabOSD1.Controls.Add(this.cbOSD);
			this.tabOSD1.Controls.Add(this.groupBox1);
			this.tabOSD1.Controls.Add(this.gbXYPositionOfTopLeftCorner);
			this.tabOSD1.Location = new System.Drawing.Point(4, 22);
			this.tabOSD1.Name = "tabOSD1";
			this.tabOSD1.Padding = new System.Windows.Forms.Padding(3);
			this.tabOSD1.Size = new System.Drawing.Size(571, 438);
			this.tabOSD1.TabIndex = 2;
			this.tabOSD1.Text = "OSD";
			this.tabOSD1.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 411);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 34;
			this.label3.Text = "Test string:";
			// 
			// txtTestString
			// 
			this.txtTestString.Location = new System.Drawing.Point(75, 407);
			this.txtTestString.Name = "txtTestString";
			this.txtTestString.Size = new System.Drawing.Size(240, 20);
			this.txtTestString.TabIndex = 33;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cbOsdRemoteButtonPress);
			this.groupBox3.Controls.Add(this.groupBox6);
			this.groupBox3.Controls.Add(this.cbOsdRemoteBatteryChange);
			this.groupBox3.Controls.Add(this.cbOsdRemoteHibernate);
			this.groupBox3.Controls.Add(this.cbOsdRemoteDisconnect);
			this.groupBox3.Controls.Add(this.cbOsdRemoteConnect);
			this.groupBox3.Controls.Add(this.cbOsdAppStart);
			this.groupBox3.Location = new System.Drawing.Point(329, 7);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(233, 375);
			this.groupBox3.TabIndex = 32;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "OSD displays when: ";
			// 
			// cbOsdRemoteButtonPress
			// 
			this.cbOsdRemoteButtonPress.AutoSize = true;
			this.cbOsdRemoteButtonPress.Location = new System.Drawing.Point(6, 159);
			this.cbOsdRemoteButtonPress.Name = "cbOsdRemoteButtonPress";
			this.cbOsdRemoteButtonPress.Size = new System.Drawing.Size(163, 17);
			this.cbOsdRemoteButtonPress.TabIndex = 12;
			this.cbOsdRemoteButtonPress.Text = "The remote button is pressed";
			this.cbOsdRemoteButtonPress.UseVisualStyleBackColor = true;
			this.cbOsdRemoteButtonPress.CheckedChanged += new System.EventHandler(this.cbOsdRemoteButtonPress_CheckedChanged);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.rbOsdRemoteButtonPressMatched);
			this.groupBox6.Controls.Add(this.rbOsdRemoteButtonPressAssigned);
			this.groupBox6.Controls.Add(this.rbOsdRemoteButtonPressAlways);
			this.groupBox6.Controls.Add(this.cbOsdAssignedKey);
			this.groupBox6.Controls.Add(this.cbOsdActiveWindowTitle);
			this.groupBox6.Controls.Add(this.cbOsdPressedRemoteButton);
			this.groupBox6.Controls.Add(this.cbOsdMappingName);
			this.groupBox6.Location = new System.Drawing.Point(16, 183);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(210, 186);
			this.groupBox6.TabIndex = 11;
			this.groupBox6.TabStop = false;
			// 
			// rbOsdRemoteButtonPressMatched
			// 
			this.rbOsdRemoteButtonPressMatched.AutoSize = true;
			this.rbOsdRemoteButtonPressMatched.Location = new System.Drawing.Point(6, 43);
			this.rbOsdRemoteButtonPressMatched.Name = "rbOsdRemoteButtonPressMatched";
			this.rbOsdRemoteButtonPressMatched.Size = new System.Drawing.Size(120, 17);
			this.rbOsdRemoteButtonPressMatched.TabIndex = 12;
			this.rbOsdRemoteButtonPressMatched.TabStop = true;
			this.rbOsdRemoteButtonPressMatched.Text = "Mapping is matched";
			this.rbOsdRemoteButtonPressMatched.UseVisualStyleBackColor = true;
			// 
			// rbOsdRemoteButtonPressAssigned
			// 
			this.rbOsdRemoteButtonPressAssigned.AutoSize = true;
			this.rbOsdRemoteButtonPressAssigned.Location = new System.Drawing.Point(6, 67);
			this.rbOsdRemoteButtonPressAssigned.Name = "rbOsdRemoteButtonPressAssigned";
			this.rbOsdRemoteButtonPressAssigned.Size = new System.Drawing.Size(98, 17);
			this.rbOsdRemoteButtonPressAssigned.TabIndex = 11;
			this.rbOsdRemoteButtonPressAssigned.TabStop = true;
			this.rbOsdRemoteButtonPressAssigned.Text = "Key is assigned";
			this.rbOsdRemoteButtonPressAssigned.UseVisualStyleBackColor = true;
			// 
			// rbOsdRemoteButtonPressAlways
			// 
			this.rbOsdRemoteButtonPressAlways.AutoSize = true;
			this.rbOsdRemoteButtonPressAlways.Location = new System.Drawing.Point(6, 20);
			this.rbOsdRemoteButtonPressAlways.Name = "rbOsdRemoteButtonPressAlways";
			this.rbOsdRemoteButtonPressAlways.Size = new System.Drawing.Size(58, 17);
			this.rbOsdRemoteButtonPressAlways.TabIndex = 10;
			this.rbOsdRemoteButtonPressAlways.TabStop = true;
			this.rbOsdRemoteButtonPressAlways.Text = "Always";
			this.rbOsdRemoteButtonPressAlways.UseVisualStyleBackColor = true;
			// 
			// cbOsdAssignedKey
			// 
			this.cbOsdAssignedKey.AutoSize = true;
			this.cbOsdAssignedKey.Location = new System.Drawing.Point(22, 163);
			this.cbOsdAssignedKey.Name = "cbOsdAssignedKey";
			this.cbOsdAssignedKey.Size = new System.Drawing.Size(118, 17);
			this.cbOsdAssignedKey.TabIndex = 9;
			this.cbOsdAssignedKey.Text = "Show assigned key";
			this.cbOsdAssignedKey.UseVisualStyleBackColor = true;
			// 
			// cbOsdActiveWindowTitle
			// 
			this.cbOsdActiveWindowTitle.AutoSize = true;
			this.cbOsdActiveWindowTitle.Location = new System.Drawing.Point(22, 91);
			this.cbOsdActiveWindowTitle.Name = "cbOsdActiveWindowTitle";
			this.cbOsdActiveWindowTitle.Size = new System.Drawing.Size(143, 17);
			this.cbOsdActiveWindowTitle.TabIndex = 6;
			this.cbOsdActiveWindowTitle.Text = "Show active window title";
			this.cbOsdActiveWindowTitle.UseVisualStyleBackColor = true;
			// 
			// cbOsdPressedRemoteButton
			// 
			this.cbOsdPressedRemoteButton.AutoSize = true;
			this.cbOsdPressedRemoteButton.Location = new System.Drawing.Point(22, 139);
			this.cbOsdPressedRemoteButton.Name = "cbOsdPressedRemoteButton";
			this.cbOsdPressedRemoteButton.Size = new System.Drawing.Size(161, 17);
			this.cbOsdPressedRemoteButton.TabIndex = 8;
			this.cbOsdPressedRemoteButton.Text = "Show pressed remote button";
			this.cbOsdPressedRemoteButton.UseVisualStyleBackColor = true;
			// 
			// cbOsdMappingName
			// 
			this.cbOsdMappingName.AutoSize = true;
			this.cbOsdMappingName.Location = new System.Drawing.Point(22, 115);
			this.cbOsdMappingName.Name = "cbOsdMappingName";
			this.cbOsdMappingName.Size = new System.Drawing.Size(125, 17);
			this.cbOsdMappingName.TabIndex = 7;
			this.cbOsdMappingName.Text = "Show mapping name";
			this.cbOsdMappingName.UseVisualStyleBackColor = true;
			// 
			// cbOsdRemoteBatteryChange
			// 
			this.cbOsdRemoteBatteryChange.AutoSize = true;
			this.cbOsdRemoteBatteryChange.Location = new System.Drawing.Point(6, 125);
			this.cbOsdRemoteBatteryChange.Name = "cbOsdRemoteBatteryChange";
			this.cbOsdRemoteBatteryChange.Size = new System.Drawing.Size(186, 17);
			this.cbOsdRemoteBatteryChange.TabIndex = 10;
			this.cbOsdRemoteBatteryChange.Text = "The remote battery life is changed";
			this.cbOsdRemoteBatteryChange.UseVisualStyleBackColor = true;
			// 
			// cbOsdRemoteHibernate
			// 
			this.cbOsdRemoteHibernate.AutoSize = true;
			this.cbOsdRemoteHibernate.Location = new System.Drawing.Point(6, 101);
			this.cbOsdRemoteHibernate.Name = "cbOsdRemoteHibernate";
			this.cbOsdRemoteHibernate.Size = new System.Drawing.Size(143, 17);
			this.cbOsdRemoteHibernate.TabIndex = 3;
			this.cbOsdRemoteHibernate.Text = "The remote is hibernated";
			this.cbOsdRemoteHibernate.UseVisualStyleBackColor = true;
			// 
			// cbOsdRemoteDisconnect
			// 
			this.cbOsdRemoteDisconnect.AutoSize = true;
			this.cbOsdRemoteDisconnect.Location = new System.Drawing.Point(6, 77);
			this.cbOsdRemoteDisconnect.Name = "cbOsdRemoteDisconnect";
			this.cbOsdRemoteDisconnect.Size = new System.Drawing.Size(157, 17);
			this.cbOsdRemoteDisconnect.TabIndex = 2;
			this.cbOsdRemoteDisconnect.Text = "The remote is disconnected";
			this.cbOsdRemoteDisconnect.UseVisualStyleBackColor = true;
			// 
			// cbOsdRemoteConnect
			// 
			this.cbOsdRemoteConnect.AutoSize = true;
			this.cbOsdRemoteConnect.Location = new System.Drawing.Point(6, 53);
			this.cbOsdRemoteConnect.Name = "cbOsdRemoteConnect";
			this.cbOsdRemoteConnect.Size = new System.Drawing.Size(144, 17);
			this.cbOsdRemoteConnect.TabIndex = 1;
			this.cbOsdRemoteConnect.Text = "The remote is connected";
			this.cbOsdRemoteConnect.UseVisualStyleBackColor = true;
			// 
			// cbOsdAppStart
			// 
			this.cbOsdAppStart.AutoSize = true;
			this.cbOsdAppStart.Location = new System.Drawing.Point(6, 20);
			this.cbOsdAppStart.Name = "cbOsdAppStart";
			this.cbOsdAppStart.Size = new System.Drawing.Size(145, 17);
			this.cbOsdAppStart.TabIndex = 0;
			this.cbOsdAppStart.Text = "This application is started";
			this.cbOsdAppStart.UseVisualStyleBackColor = true;
			// 
			// buttonTestOsd
			// 
			this.buttonTestOsd.Location = new System.Drawing.Point(329, 405);
			this.buttonTestOsd.Name = "buttonTestOsd";
			this.buttonTestOsd.Size = new System.Drawing.Size(233, 25);
			this.buttonTestOsd.TabIndex = 26;
			this.buttonTestOsd.Text = "Test OSD";
			this.buttonTestOsd.UseVisualStyleBackColor = true;
			this.buttonTestOsd.Click += new System.EventHandler(this.buttonTestOsd_Click);
			// 
			// cbOSD
			// 
			this.cbOSD.AutoSize = true;
			this.cbOSD.Location = new System.Drawing.Point(13, 17);
			this.cbOSD.Name = "cbOSD";
			this.cbOSD.Size = new System.Drawing.Size(187, 17);
			this.cbOSD.TabIndex = 31;
			this.cbOSD.Text = "OSD (On Screen Display) enabled";
			this.cbOSD.UseVisualStyleBackColor = true;
			this.cbOSD.CheckedChanged += new System.EventHandler(this.cbOSD_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.buttonFontPick);
			this.groupBox1.Controls.Add(this.buttonPathColorPick);
			this.groupBox1.Controls.Add(this.buttonTextColorPick);
			this.groupBox1.Controls.Add(this.nudAlpha);
			this.groupBox1.Controls.Add(this.lblAlpha);
			this.groupBox1.Controls.Add(this.lblTextTime);
			this.groupBox1.Controls.Add(this.nudTextTime);
			this.groupBox1.Location = new System.Drawing.Point(6, 182);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(309, 219);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Visual Settings";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbAnimateEffect);
			this.groupBox2.Controls.Add(this.lblAnimateEffect);
			this.groupBox2.Controls.Add(this.nudAnimateTime);
			this.groupBox2.Controls.Add(this.lblAnimateTimeDescription);
			this.groupBox2.Controls.Add(this.lblAnimateTime);
			this.groupBox2.Location = new System.Drawing.Point(8, 105);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(290, 107);
			this.groupBox2.TabIndex = 30;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Animation Settings";
			// 
			// cbAnimateEffect
			// 
			this.cbAnimateEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAnimateEffect.FormattingEnabled = true;
			this.cbAnimateEffect.Location = new System.Drawing.Point(95, 20);
			this.cbAnimateEffect.Name = "cbAnimateEffect";
			this.cbAnimateEffect.Size = new System.Drawing.Size(135, 21);
			this.cbAnimateEffect.TabIndex = 5;
			// 
			// lblAnimateEffect
			// 
			this.lblAnimateEffect.AutoSize = true;
			this.lblAnimateEffect.Location = new System.Drawing.Point(6, 23);
			this.lblAnimateEffect.Name = "lblAnimateEffect";
			this.lblAnimateEffect.Size = new System.Drawing.Size(78, 13);
			this.lblAnimateEffect.TabIndex = 13;
			this.lblAnimateEffect.Text = "Animate effect:";
			// 
			// nudAnimateTime
			// 
			this.nudAnimateTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudAnimateTime.Location = new System.Drawing.Point(172, 48);
			this.nudAnimateTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudAnimateTime.Name = "nudAnimateTime";
			this.nudAnimateTime.Size = new System.Drawing.Size(58, 20);
			this.nudAnimateTime.TabIndex = 4;
			// 
			// lblAnimateTimeDescription
			// 
			this.lblAnimateTimeDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblAnimateTimeDescription.ForeColor = System.Drawing.Color.Brown;
			this.lblAnimateTimeDescription.Location = new System.Drawing.Point(20, 75);
			this.lblAnimateTimeDescription.Name = "lblAnimateTimeDescription";
			this.lblAnimateTimeDescription.Size = new System.Drawing.Size(264, 29);
			this.lblAnimateTimeDescription.TabIndex = 20;
			this.lblAnimateTimeDescription.Text = "If this value = 0, \"Animate effect\" will be ignored and \r\nOSD window will be disp" +
    "layed and vanished at once.";
			// 
			// lblAnimateTime
			// 
			this.lblAnimateTime.AutoSize = true;
			this.lblAnimateTime.Location = new System.Drawing.Point(6, 50);
			this.lblAnimateTime.Name = "lblAnimateTime";
			this.lblAnimateTime.Size = new System.Drawing.Size(124, 13);
			this.lblAnimateTime.TabIndex = 16;
			this.lblAnimateTime.Text = "Time for animate (in ms): ";
			// 
			// buttonFontPick
			// 
			this.buttonFontPick.AutoSize = true;
			this.buttonFontPick.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonFontPick.Location = new System.Drawing.Point(6, 20);
			this.buttonFontPick.Name = "buttonFontPick";
			this.buttonFontPick.Size = new System.Drawing.Size(82, 25);
			this.buttonFontPick.TabIndex = 7;
			this.buttonFontPick.Text = "Font of text...";
			this.buttonFontPick.UseVisualStyleBackColor = true;
			this.buttonFontPick.Click += new System.EventHandler(this.buttonFontPick_Click);
			// 
			// buttonPathColorPick
			// 
			this.buttonPathColorPick.AutoSize = true;
			this.buttonPathColorPick.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonPathColorPick.Location = new System.Drawing.Point(186, 20);
			this.buttonPathColorPick.Name = "buttonPathColorPick";
			this.buttonPathColorPick.Size = new System.Drawing.Size(112, 25);
			this.buttonPathColorPick.TabIndex = 29;
			this.buttonPathColorPick.Text = "Color of text path...";
			this.buttonPathColorPick.UseVisualStyleBackColor = true;
			this.buttonPathColorPick.Click += new System.EventHandler(this.buttonPathColorPick_Click);
			// 
			// buttonTextColorPick
			// 
			this.buttonTextColorPick.AutoSize = true;
			this.buttonTextColorPick.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonTextColorPick.Location = new System.Drawing.Point(94, 20);
			this.buttonTextColorPick.Name = "buttonTextColorPick";
			this.buttonTextColorPick.Size = new System.Drawing.Size(86, 25);
			this.buttonTextColorPick.TabIndex = 6;
			this.buttonTextColorPick.Text = "Color of text...";
			this.buttonTextColorPick.UseVisualStyleBackColor = true;
			this.buttonTextColorPick.Click += new System.EventHandler(this.buttonTextColorPick_Click);
			// 
			// nudAlpha
			// 
			this.nudAlpha.Location = new System.Drawing.Point(189, 51);
			this.nudAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudAlpha.Name = "nudAlpha";
			this.nudAlpha.Size = new System.Drawing.Size(49, 20);
			this.nudAlpha.TabIndex = 2;
			// 
			// lblAlpha
			// 
			this.lblAlpha.AutoSize = true;
			this.lblAlpha.Location = new System.Drawing.Point(6, 53);
			this.lblAlpha.Name = "lblAlpha";
			this.lblAlpha.Size = new System.Drawing.Size(152, 13);
			this.lblAlpha.TabIndex = 5;
			this.lblAlpha.Text = "Transparency of text (0 - 255): ";
			// 
			// lblTextTime
			// 
			this.lblTextTime.AutoSize = true;
			this.lblTextTime.Location = new System.Drawing.Point(6, 80);
			this.lblTextTime.Name = "lblTextTime";
			this.lblTextTime.Size = new System.Drawing.Size(139, 13);
			this.lblTextTime.TabIndex = 11;
			this.lblTextTime.Text = "Time to display text  (in ms): ";
			// 
			// nudTextTime
			// 
			this.nudTextTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudTextTime.Location = new System.Drawing.Point(180, 78);
			this.nudTextTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudTextTime.Name = "nudTextTime";
			this.nudTextTime.Size = new System.Drawing.Size(58, 20);
			this.nudTextTime.TabIndex = 3;
			// 
			// gbXYPositionOfTopLeftCorner
			// 
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.cbXYSetting);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.gbVerticalAlign);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.gbAlign);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.label7);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.nudX);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.nudY);
			this.gbXYPositionOfTopLeftCorner.Controls.Add(this.label8);
			this.gbXYPositionOfTopLeftCorner.Location = new System.Drawing.Point(6, 46);
			this.gbXYPositionOfTopLeftCorner.Name = "gbXYPositionOfTopLeftCorner";
			this.gbXYPositionOfTopLeftCorner.Size = new System.Drawing.Size(309, 130);
			this.gbXYPositionOfTopLeftCorner.TabIndex = 28;
			this.gbXYPositionOfTopLeftCorner.TabStop = false;
			this.gbXYPositionOfTopLeftCorner.Text = "Position Settings";
			// 
			// cbXYSetting
			// 
			this.cbXYSetting.AutoSize = true;
			this.cbXYSetting.Location = new System.Drawing.Point(205, 26);
			this.cbXYSetting.Name = "cbXYSetting";
			this.cbXYSetting.Size = new System.Drawing.Size(103, 17);
			this.cbXYSetting.TabIndex = 26;
			this.cbXYSetting.Text = "Custom Setting: ";
			this.cbXYSetting.UseVisualStyleBackColor = true;
			this.cbXYSetting.CheckedChanged += new System.EventHandler(this.cbXYSetting_CheckedChanged);
			// 
			// gbVerticalAlign
			// 
			this.gbVerticalAlign.Controls.Add(this.rbBottom);
			this.gbVerticalAlign.Controls.Add(this.rbMiddle);
			this.gbVerticalAlign.Controls.Add(this.rbTop);
			this.gbVerticalAlign.Location = new System.Drawing.Point(107, 20);
			this.gbVerticalAlign.Name = "gbVerticalAlign";
			this.gbVerticalAlign.Size = new System.Drawing.Size(95, 105);
			this.gbVerticalAlign.TabIndex = 24;
			this.gbVerticalAlign.TabStop = false;
			this.gbVerticalAlign.Text = "Vertical Align";
			// 
			// rbBottom
			// 
			this.rbBottom.AutoSize = true;
			this.rbBottom.Location = new System.Drawing.Point(6, 76);
			this.rbBottom.Name = "rbBottom";
			this.rbBottom.Size = new System.Drawing.Size(58, 17);
			this.rbBottom.TabIndex = 23;
			this.rbBottom.Text = "Bottom";
			this.rbBottom.UseVisualStyleBackColor = true;
			this.rbBottom.CheckedChanged += new System.EventHandler(this.rbVerticalAlign_CheckedChanged);
			// 
			// rbMiddle
			// 
			this.rbMiddle.AutoSize = true;
			this.rbMiddle.Location = new System.Drawing.Point(6, 49);
			this.rbMiddle.Name = "rbMiddle";
			this.rbMiddle.Size = new System.Drawing.Size(56, 17);
			this.rbMiddle.TabIndex = 22;
			this.rbMiddle.Text = "Middle";
			this.rbMiddle.UseVisualStyleBackColor = true;
			this.rbMiddle.CheckedChanged += new System.EventHandler(this.rbVerticalAlign_CheckedChanged);
			// 
			// rbTop
			// 
			this.rbTop.AutoSize = true;
			this.rbTop.Location = new System.Drawing.Point(6, 22);
			this.rbTop.Name = "rbTop";
			this.rbTop.Size = new System.Drawing.Size(44, 17);
			this.rbTop.TabIndex = 21;
			this.rbTop.Text = "Top";
			this.rbTop.UseVisualStyleBackColor = true;
			this.rbTop.CheckedChanged += new System.EventHandler(this.rbVerticalAlign_CheckedChanged);
			// 
			// gbAlign
			// 
			this.gbAlign.Controls.Add(this.rbRight);
			this.gbAlign.Controls.Add(this.rbCenter);
			this.gbAlign.Controls.Add(this.rbLeft);
			this.gbAlign.Location = new System.Drawing.Point(6, 20);
			this.gbAlign.Name = "gbAlign";
			this.gbAlign.Size = new System.Drawing.Size(95, 105);
			this.gbAlign.TabIndex = 14;
			this.gbAlign.TabStop = false;
			this.gbAlign.Text = "Align";
			// 
			// rbRight
			// 
			this.rbRight.AutoSize = true;
			this.rbRight.Location = new System.Drawing.Point(6, 76);
			this.rbRight.Name = "rbRight";
			this.rbRight.Size = new System.Drawing.Size(50, 17);
			this.rbRight.TabIndex = 23;
			this.rbRight.Text = "Right";
			this.rbRight.UseVisualStyleBackColor = true;
			this.rbRight.CheckedChanged += new System.EventHandler(this.rbAlign_CheckedChanged);
			// 
			// rbCenter
			// 
			this.rbCenter.AutoSize = true;
			this.rbCenter.Location = new System.Drawing.Point(6, 49);
			this.rbCenter.Name = "rbCenter";
			this.rbCenter.Size = new System.Drawing.Size(56, 17);
			this.rbCenter.TabIndex = 22;
			this.rbCenter.Text = "Center";
			this.rbCenter.UseVisualStyleBackColor = true;
			this.rbCenter.CheckedChanged += new System.EventHandler(this.rbAlign_CheckedChanged);
			// 
			// rbLeft
			// 
			this.rbLeft.AutoSize = true;
			this.rbLeft.Location = new System.Drawing.Point(6, 22);
			this.rbLeft.Name = "rbLeft";
			this.rbLeft.Size = new System.Drawing.Size(43, 17);
			this.rbLeft.TabIndex = 21;
			this.rbLeft.Text = "Left";
			this.rbLeft.UseVisualStyleBackColor = true;
			this.rbLeft.CheckedChanged += new System.EventHandler(this.rbAlign_CheckedChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(220, 77);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(16, 15);
			this.label7.TabIndex = 25;
			this.label7.Text = "Y:";
			// 
			// nudX
			// 
			this.nudX.Location = new System.Drawing.Point(244, 50);
			this.nudX.Name = "nudX";
			this.nudX.Size = new System.Drawing.Size(53, 20);
			this.nudX.TabIndex = 0;
			// 
			// nudY
			// 
			this.nudY.Location = new System.Drawing.Point(244, 77);
			this.nudY.Name = "nudY";
			this.nudY.Size = new System.Drawing.Size(53, 20);
			this.nudY.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(220, 50);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 15);
			this.label8.TabIndex = 3;
			this.label8.Text = "X:";
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(591, 478);
			this.Controls.Add(this.TabControl);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SettingsForm";
			this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.Text = "PS3BluMote configuration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
			this.Shown += new System.EventHandler(this.SettingsForm_Shown);
			this.menuNotifyIcon.ResumeLayout(false);
			this.TabControl.ResumeLayout(false);
			this.tabMappings.ResumeLayout(false);
			this.tabMappings.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.gbAdvanced.ResumeLayout(false);
			this.gbAdvanced.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.tabOSD1.ResumeLayout(false);
			this.tabOSD1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudAnimateTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTextTime)).EndInit();
			this.gbXYPositionOfTopLeftCorner.ResumeLayout(false);
			this.gbXYPositionOfTopLeftCorner.PerformLayout();
			this.gbVerticalAlign.ResumeLayout(false);
			this.gbVerticalAlign.PerformLayout();
			this.gbAlign.ResumeLayout(false);
			this.gbAlign.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip menuNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem mitemSettings;
        private System.Windows.Forms.ToolStripMenuItem mitemExit;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabMappings;
        private System.Windows.Forms.ToolTip toolTipAdvanced;
        private System.Windows.Forms.FontDialog dialogFont;
        private System.Windows.Forms.ColorDialog dialogTextColor;
        private System.Windows.Forms.ColorDialog dialogPathColor;
        private System.Windows.Forms.TabPage tabOSD1;
        private System.Windows.Forms.Button buttonPathColorPick;
        private System.Windows.Forms.GroupBox gbXYPositionOfTopLeftCorner;
        private System.Windows.Forms.CheckBox cbXYSetting;
        private System.Windows.Forms.GroupBox gbVerticalAlign;
        private System.Windows.Forms.RadioButton rbBottom;
        private System.Windows.Forms.RadioButton rbMiddle;
        private System.Windows.Forms.RadioButton rbTop;
        private System.Windows.Forms.GroupBox gbAlign;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonTestOsd;
        private System.Windows.Forms.NumericUpDown nudAnimateTime;
        private System.Windows.Forms.Label lblAnimateTime;
        private System.Windows.Forms.ComboBox cbAnimateEffect;
        private System.Windows.Forms.Label lblAnimateEffect;
        private System.Windows.Forms.NumericUpDown nudTextTime;
        private System.Windows.Forms.Label lblTextTime;
        private System.Windows.Forms.Button buttonFontPick;
        private System.Windows.Forms.Button buttonTextColorPick;
        private System.Windows.Forms.NumericUpDown nudAlpha;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.Label lblAnimateTimeDescription;
        private System.Windows.Forms.ListBox lbApps;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpper;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonLower;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonBottom;
        private System.Windows.Forms.Button buttonTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbOSD;
        private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel llblOpenFolder;
        private System.Windows.Forms.CheckBox cbDebugMode;
		private System.Windows.Forms.GroupBox gbAdvanced;
        private System.Windows.Forms.Label lblRemoteCodes;
        private System.Windows.Forms.TextBox txtVendorId;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label lblVendorId;
		private System.Windows.Forms.Label lblProductId;
        private System.Windows.Forms.CheckBox cbSms;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbOsdAppStart;
        private System.Windows.Forms.CheckBox cbOsdRemoteHibernate;
        private System.Windows.Forms.CheckBox cbOsdRemoteDisconnect;
        private System.Windows.Forms.CheckBox cbOsdRemoteConnect;
        private System.Windows.Forms.CheckBox cbOsdAssignedKey;
        private System.Windows.Forms.CheckBox cbOsdPressedRemoteButton;
        private System.Windows.Forms.CheckBox cbOsdMappingName;
        private System.Windows.Forms.CheckBox cbOsdActiveWindowTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTestString;
        private System.Windows.Forms.CheckBox cbOsdRemoteBatteryChange;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbOsdRemoteButtonPressMatched;
        private System.Windows.Forms.RadioButton rbOsdRemoteButtonPressAssigned;
        private System.Windows.Forms.RadioButton rbOsdRemoteButtonPressAlways;
        private System.Windows.Forms.CheckBox cbOsdRemoteButtonPress;
		private System.Windows.Forms.CheckBox cbStartup;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.TextBox txtRepeatDelay;
		private System.Windows.Forms.Label lblRepeatDelay;
		private System.Windows.Forms.CheckBox cbCustomRepeatRate;
		private System.Windows.Forms.TextBox txtRepeatInterval;
		private System.Windows.Forms.Label lblRepeatInterval;
	}
}