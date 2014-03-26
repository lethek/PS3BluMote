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

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PS3BluMote
{
    public partial class MappingsForm : Form
    {
        public AppNode appMapping;
        private bool canHook;

        public MappingsForm(AppNode appMapping)
        {
            InitializeComponent();

            this.appMapping = appMapping;

            txtSettingName.Text = this.appMapping.name;
            txtCondition.Text = this.appMapping.condition;
            cbCaseSensitive.Checked = (this.appMapping.caseSensitive == true);

            foreach (SendInputAPI.Keyboard.KeyCode key in Enum.GetValues(typeof(SendInputAPI.Keyboard.KeyCode)))
            {
                ListViewItem listViewItem = new ListViewItem(key.ToString());
                listViewItem.Name = key.ToString();
                lvKeys.Items.Add(listViewItem);
            }

            ListViewItem lvItem;
            foreach (var buttonNode in this.appMapping.buttonMappings)
            {
                lvItem = new ListViewItem();
                lvItem.SubItems.Add(buttonNode.name);
                lvItem.SubItems.Add(buttonNode.joinedKeyMapped.Replace(",", " + "));
                lvButtons.Items.Add(lvItem);

                int index = (int)Enum.Parse(typeof(PS3Remote.Button), buttonNode.name);
                lvButtons.Items[index].Checked = buttonNode.repeat;
            }
        }

        private void MappingsForm_Shown(object sender, EventArgs e)
        {
            lvButtons.Items[0].Selected = true;
        }

        private void MappingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtSettingName.Text == "" || txtCondition.Text == "")
            {
                MessageBox.Show("Invalid Mappings name or Window title.");
                e.Cancel = true;
            }
            else
            {
                if (ptrHook != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(ptrHook);
                    ptrHook = IntPtr.Zero;
                }
            }
        }
        
        private void MappingsForm_Activated(object sender, EventArgs e)
        {
            canHook = true;
        }

        private void MappingsForm_Deactivate(object sender, EventArgs e)
        {
            canHook = false;
        }
        
        private void lvButtons_Enter(object sender, EventArgs e)
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(WH_KEYBOARD_LL, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        private void lvButtons_Leave(object sender, EventArgs e)
        {
            if (ptrHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(ptrHook);
                ptrHook = IntPtr.Zero;
            }
        }

        private void txtSettingName_TextChanged(object sender, EventArgs e)
        {
            appMapping.name = txtSettingName.Text;
        }

        private void txtCondition_TextChanged(object sender, EventArgs e)
        {
            appMapping.condition = txtCondition.Text;
        }

        private void cbCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            appMapping.caseSensitive = cbCaseSensitive.Checked;
        }

        private void lvButtons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvButtons.SelectedItems.Count == 0) return;

            lvButtons.Tag = true;

            int index = (int)Enum.Parse(typeof(PS3Remote.Button), lvButtons.SelectedItems[0].SubItems[1].Text, true);
            List<SendInputAPI.Keyboard.KeyCode> mappedKeys = appMapping.buttonMappings[index].keysMapped;

            foreach (ListViewItem lvItem in lvKeys.Items)
            {
                if (mappedKeys.Contains((SendInputAPI.Keyboard.KeyCode)Enum.Parse(typeof(SendInputAPI.Keyboard.KeyCode), lvItem.Text, true)))
                {
                    lvItem.Checked = true;
                }
                else
                {
                    lvItem.Checked = false;
                }
            }

            lvButtons.Tag = false;
        }

        private void lvButtons_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            appMapping.buttonMappings[e.Item.Index].repeat = e.Item.Checked;
        }

        private void lvKeys_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if ((bool)lvButtons.Tag) return;

            int index = (int)Enum.Parse(typeof(PS3Remote.Button), lvButtons.SelectedItems[0].SubItems[1].Text, true);
            List<SendInputAPI.Keyboard.KeyCode> mappedKeys = appMapping.buttonMappings[index].keysMapped;
            SendInputAPI.Keyboard.KeyCode code = (SendInputAPI.Keyboard.KeyCode)Enum.Parse(typeof(SendInputAPI.Keyboard.KeyCode), lvKeys.Items[e.Index].Text, true);

            if (e.NewValue == CheckState.Checked && !mappedKeys.Contains(code))
            {
                mappedKeys.Add(code);
            }
            else
            {
                mappedKeys.Remove(code);
            }

            String text = "";
            foreach (SendInputAPI.Keyboard.KeyCode key in mappedKeys)
            {
                text += key.ToString() + " + ";
            }

            lvButtons.SelectedItems[0].SubItems[2].Text = (mappedKeys.Count > 0) ? text.Substring(0, text.Length - 3) : "";
        }

        /*
        private void lvButtons_KeyDown(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(Enum.GetName(typeof(SendInputAPI.Keyboard.KeyCode), e.KeyValue), "KeyDown Event");

            if (e.KeyCode == Keys.Enter)
            {
                ListView.SelectedListViewItemCollection selectedItems = lvButtons.SelectedItems;
                if (selectedItems.Count > 0)
                {
                    int index = selectedItems[0].Index;
                    if (index < lvButtons.Items.Count)
                    {
                        int nextIndex = index + 1;
                        lvButtons.Items[nextIndex].Selected = true;
                        lvButtons.Items[nextIndex].Focused = true;
                    }
                }
                else
                {
                    lvButtons.Items[0].Selected = true;
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                for (int i = 0; i < lvKeys.Items.Count; i++)
                {
                    lvKeys.Items[i].Checked = false;
                }
                lvButtons.SelectedItems[0].Checked = false;
            }
            else
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                ListViewItem[] foundItems = lvKeys.Items.Find(Enum.GetName(typeof(SendInputAPI.Keyboard.KeyCode), e.KeyValue), true);
                if (foundItems.Length > 0)
                {
                    foundItems[0].Checked = true;
                }
            }
        }
        */
        // prevent Win key
        private void lvButtons_KeyDown(Keys key)
        {
            if (key.Equals(Keys.Enter) || key.Equals(Keys.Down))
            {
                ListView.SelectedListViewItemCollection selectedItems = lvButtons.SelectedItems;
                if (selectedItems.Count > 0)
                {
                    int index = selectedItems[0].Index;
                    if (index < lvButtons.Items.Count)
                    {
                        int nextIndex = index + 1;
                        lvButtons.Items[nextIndex].Selected = true;
                        lvButtons.Items[nextIndex].Focused = true;
                    }
                }
                else
                {
                    lvButtons.Items[0].Selected = true;
                }
            }
            else if (key.Equals(Keys.Up))
            {
                ListView.SelectedListViewItemCollection selectedItems = lvButtons.SelectedItems;
                if (selectedItems.Count > 0)
                {
                    int index = selectedItems[0].Index;
                    if (index > 0)
                    {
                        int prevIndex = index - 1;
                        lvButtons.Items[prevIndex].Selected = true;
                        lvButtons.Items[prevIndex].Focused = true;
                    }
                }
                else
                {
                    lvButtons.Items[0].Selected = true;
                }
            }
            else if (key.Equals(Keys.Delete) || key.Equals(Keys.Back))
            {
                for (int i = 0; i < lvKeys.Items.Count; i++)
                {
                    lvKeys.Items[i].Checked = false;
                }
                lvButtons.SelectedItems[0].Checked = false;
            }
            else
            {
                ListViewItem[] foundItems = lvKeys.Items.Find(Enum.GetName(typeof(SendInputAPI.Keyboard.KeyCode), key), true);
                if (foundItems.Length > 0)
                {
                    foundItems[0].Checked = true;
                }
            }
        }
        // prevent Win key

        # region ## prevent Win key DLL inmport ##
        // Declaring Global objects
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private const int WH_KEYBOARD_LL = 13;
        // private const int WM_KEYUP = 0x0101;
        private const int WM_KEYDOWN = 0x0100;
        // private const int WM_SYSKEYUP = 0x0105;
        private const int WM_SYSKEYDOWN = 0x0104;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (canHook && lvButtons.Focused)
            {
                // if (nCode >= 0 && (wp == (IntPtr)WM_KEYUP || wp == (IntPtr)WM_SYSKEYUP))
                if (nCode >= 0 && (wp == (IntPtr)WM_KEYDOWN || wp == (IntPtr)WM_SYSKEYDOWN))
                {
                    KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));
                    lvButtons_KeyDown(objKeyInfo.key);
                }
                return new IntPtr(1);
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        // System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        # endregion
    }
}
