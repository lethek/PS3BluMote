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
    partial class MappingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingsForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvButtons = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvKeys = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSettingName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(6, 34);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lvButtons);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lvKeys);
            this.splitContainer.Size = new System.Drawing.Size(721, 413);
            this.splitContainer.SplitterDistance = 331;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 3;
            // 
            // lvButtons
            // 
            this.lvButtons.CheckBoxes = true;
            this.lvButtons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.lvButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvButtons.FullRowSelect = true;
            this.lvButtons.GridLines = true;
            this.lvButtons.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvButtons.HideSelection = false;
            this.lvButtons.Location = new System.Drawing.Point(0, 0);
            this.lvButtons.MultiSelect = false;
            this.lvButtons.Name = "lvButtons";
            this.lvButtons.ShowGroups = false;
            this.lvButtons.Size = new System.Drawing.Size(331, 413);
            this.lvButtons.TabIndex = 0;
            this.lvButtons.UseCompatibleStateImageBehavior = false;
            this.lvButtons.View = System.Windows.Forms.View.Details;
            this.lvButtons.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvButtons_ItemChecked);
            this.lvButtons.SelectedIndexChanged += new System.EventHandler(this.lvButtons_SelectedIndexChanged);
            this.lvButtons.Enter += new System.EventHandler(this.lvButtons_Enter);
            this.lvButtons.Leave += new System.EventHandler(this.lvButtons_Leave);
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 2;
            this.columnHeader3.Text = "Repeat";
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 0;
            this.columnHeader1.Text = "Remote button";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 1;
            this.columnHeader2.Text = "Keys assigned";
            this.columnHeader2.Width = 150;
            // 
            // lvKeys
            // 
            this.lvKeys.CheckBoxes = true;
            this.lvKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvKeys.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvKeys.Location = new System.Drawing.Point(0, 0);
            this.lvKeys.MultiSelect = false;
            this.lvKeys.Name = "lvKeys";
            this.lvKeys.ShowGroups = false;
            this.lvKeys.Size = new System.Drawing.Size(384, 413);
            this.lvKeys.TabIndex = 1;
            this.lvKeys.UseCompatibleStateImageBehavior = false;
            this.lvKeys.View = System.Windows.Forms.View.List;
            this.lvKeys.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvKeys_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mappings name:";
            // 
            // txtSettingName
            // 
            this.txtSettingName.Location = new System.Drawing.Point(101, 9);
            this.txtSettingName.Name = "txtSettingName";
            this.txtSettingName.Size = new System.Drawing.Size(149, 19);
            this.txtSettingName.TabIndex = 5;
            this.txtSettingName.TextChanged += new System.EventHandler(this.txtSettingName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Window title (partial / regular expression):";
            // 
            // txtCondition
            // 
            this.txtCondition.Location = new System.Drawing.Point(497, 8);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(127, 19);
            this.txtCondition.TabIndex = 7;
            this.txtCondition.TextChanged += new System.EventHandler(this.txtCondition_TextChanged);
            // 
            // cbCaseSensitive
            // 
            this.cbCaseSensitive.AutoSize = true;
            this.cbCaseSensitive.Location = new System.Drawing.Point(630, 11);
            this.cbCaseSensitive.Name = "cbCaseSensitive";
            this.cbCaseSensitive.Size = new System.Drawing.Size(98, 16);
            this.cbCaseSensitive.TabIndex = 8;
            this.cbCaseSensitive.Text = "case sensitive";
            this.cbCaseSensitive.UseVisualStyleBackColor = true;
            this.cbCaseSensitive.CheckedChanged += new System.EventHandler(this.cbCaseSensitive_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(4, 450);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(726, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "You can assign keys from keyboard except Enter, Del and BS, Arrow Up and Down and" +
    " \"Ctrl + Alt + Del\" combination.";
            // 
            // MappingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 468);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbCaseSensitive);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSettingName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MappingsForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowInTaskbar = false;
            this.Text = "PS3BluMote configuration";
            this.Activated += new System.EventHandler(this.MappingsForm_Activated);
            this.Deactivate += new System.EventHandler(this.MappingsForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MappingsForm_FormClosing);
            this.Shown += new System.EventHandler(this.MappingsForm_Shown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView lvButtons;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvKeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSettingName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.CheckBox cbCaseSensitive;
        private System.Windows.Forms.Label label3;
    }
}