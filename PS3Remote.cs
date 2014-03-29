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

using System;
using System.Collections.Generic;
using System.Timers;
//using System.Diagnostics;

using HidLibrary;
using WindowsAPI;
using NLog;

namespace PS3BluMote
{
    class PS3Remote
    {
        public event EventHandler<EventArgs> BatteryLifeChanged;
        public event EventHandler<ButtonData> ButtonDown;
        public event EventHandler<ButtonData> ButtonReleased;
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<EventArgs> Disconnected;

        private HidDevice hidRemote = null;
        private Timer timerFindRemote = null;
        private Timer timerHibernation = null;
        private int vendorId = 0x054c;
        private int productId = 0x0306;
        private ButtonType lastButton = ButtonType.Angle;
        private bool isButtonDown = false;

	    private static readonly Dictionary<ButtonCode, ButtonType> buttonCodes =  new Dictionary<ButtonCode, ButtonType>() {
	        { new ButtonCode(0, 0, 0, 22), ButtonType.Eject },     //Eject
	        { new ButtonCode(0, 0, 0, 100), ButtonType.Audio },    //Audio
	        { new ButtonCode(0, 0, 0, 101), ButtonType.Angle },   //Angle
	        { new ButtonCode(0, 0, 0, 99), ButtonType.Subtitle },     //Subtitle
	        { new ButtonCode(0, 0, 0, 15), ButtonType.Clear },     //Clear
	        { new ButtonCode(0, 0, 0, 40), ButtonType.Time },     //Time
	        { new ButtonCode(0, 0, 0, 0), ButtonType.NUM_1 },      //NUM_1
	        { new ButtonCode(0, 0, 0, 1), ButtonType.NUM_2 },      //NUM_2
	        { new ButtonCode(0, 0, 0, 2), ButtonType.NUM_3 },      //NUM_3
	        { new ButtonCode(0, 0, 0, 3), ButtonType.NUM_4 },      //NUM_4
	        { new ButtonCode(0, 0, 0, 4), ButtonType.NUM_5 },      //NUM_5
	        { new ButtonCode(0, 0, 0, 5), ButtonType.NUM_6 },      //NUM_6
	        { new ButtonCode(0, 0, 0, 6), ButtonType.NUM_7 },      //NUM_7
	        { new ButtonCode(0, 0, 0, 7), ButtonType.NUM_8 },      //NUM_8
	        { new ButtonCode(0, 0, 0, 8), ButtonType.NUM_9 },      //NUM_9
	        { new ButtonCode(0, 0, 0, 9), ButtonType.NUM_0 },      //NUM_0
	        { new ButtonCode(0, 0, 0, 128), ButtonType.Blue },    //Blue
	        { new ButtonCode(0, 0, 0, 129), ButtonType.Red },    //Red
	        { new ButtonCode(0, 0, 0, 130), ButtonType.Green },    //Green
	        { new ButtonCode(0, 0, 0, 131), ButtonType.Yellow },    //Yellow
	        { new ButtonCode(0, 0, 0, 112), ButtonType.Display },    //Display
	        { new ButtonCode(0, 0, 0, 26), ButtonType.Top_Menu },     //Top_Menu
	        { new ButtonCode(0, 0, 0, 64), ButtonType.PopUp_Menu },     //PopUp_Menu
	        { new ButtonCode(0, 0, 0, 14), ButtonType.Return },     //Return
	        { new ButtonCode(0, 16, 0, 92), ButtonType.Triangle },    //Triangle
	        { new ButtonCode(0, 32, 0, 93), ButtonType.Circle },    //Circle
	        { new ButtonCode(0, 128, 0, 95), ButtonType.Square },   //Square
	        { new ButtonCode(0, 64, 0, 94), ButtonType.Cross },    //Cross
	        { new ButtonCode(16, 0, 0, 84), ButtonType.Arrow_Up },    //Arrow_Up
	        { new ButtonCode(64, 0, 0, 86), ButtonType.Arrow_Down },    //Arrow_Down
	        { new ButtonCode(128, 0, 0, 87), ButtonType.Arrow_Left },   //Arrow_Left
	        { new ButtonCode(32, 0, 0, 85), ButtonType.Arrow_Right },    //Arrow_Right
	        { new ButtonCode(0, 0, 8, 11), ButtonType.Enter },     //Enter
	        { new ButtonCode(0, 4, 0, 90), ButtonType.L1 },     //L1
	        { new ButtonCode(0, 1, 0, 88), ButtonType.L2 },     //L2
	        { new ButtonCode(2, 0, 0, 81), ButtonType.L3 },     //L3
	        { new ButtonCode(0, 8, 0, 91), ButtonType.R1 },     //R1
	        { new ButtonCode(0, 2, 0, 89), ButtonType.R2 },     //R2
	        { new ButtonCode(4, 0, 0, 82), ButtonType.R3 },     //R3
	        { new ButtonCode(0, 0, 1, 67), ButtonType.Playstation },     //Playstation
	        { new ButtonCode(1, 0, 0, 80), ButtonType.Select },     //Select
	        { new ButtonCode(8, 0, 0, 83), ButtonType.Start },     //Start
	        { new ButtonCode(0, 0, 0, 50), ButtonType.Play },     //Play
	        { new ButtonCode(0, 0, 0, 56), ButtonType.Stop },     //Stop
	        { new ButtonCode(0, 0, 0, 57), ButtonType.Pause },     //Pause
	        { new ButtonCode(0, 0, 0, 51), ButtonType.Scan_Back },     //Scan_Back
	        { new ButtonCode(0, 0, 0, 52), ButtonType.Scan_Forward },     //Scan_Forward
	        { new ButtonCode(0, 0, 0, 48), ButtonType.Prev },     //Prev
	        { new ButtonCode(0, 0, 0, 49), ButtonType.Next },     //Next
	        { new ButtonCode(0, 0, 0, 96), ButtonType.Step_Back },     //Step_Back
	        { new ButtonCode(0, 0, 0, 97), ButtonType.Step_Forward },     //Step_Forward
            { new ButtonCode(0, 0, 0, 118), ButtonType.Instant_Back },    //instant back
            { new ButtonCode(0, 0, 0, 117), ButtonType.Instant_Forward },    //instant fwd
            { new ButtonCode(0, 0, 0, 16), ButtonType.Channel_Up },     //channel up
            { new ButtonCode(0, 0, 0, 17), ButtonType.Channel_Down },     //channel down
            { new ButtonCode(0, 0, 0, 12), ButtonType.dash_slash_dash_dash }      // "-/--" dash_slash_dash_dash
	    };

	    private bool _hibernationEnabled;

	    private byte _batteryLife = 101;

	    #region "Remote button codes"

	    #endregion

        public PS3Remote(int vendor, int product, bool hibernation)
        {
            vendorId = vendor;
            productId = product;
            _hibernationEnabled = hibernation;

            timerHibernation = new Timer();
            timerHibernation.Interval = 60000;
            timerHibernation.Elapsed += new ElapsedEventHandler(timerHibernation_Elapsed);

            timerFindRemote = new Timer();
            timerFindRemote.Interval = 1500;
            timerFindRemote.Elapsed += new ElapsedEventHandler(timerFindRemote_Elapsed);
        }

        public void connect()
        {
            timerFindRemote.Enabled = true;
        }

        public byte getBatteryLife
        {
            get { return _batteryLife; }
        }

	    public string getBatteryLifeString()
	    {
		    if (_batteryLife > 100) {
			    return "Unknown";
		    }
		    return getBatteryLife + "%";
	    }

        public bool isConnected
        {
            get { return timerFindRemote.Enabled; }
        }

        public bool hibernationEnabled
        {
            get { return _hibernationEnabled; }
            set { _hibernationEnabled = value; }
        }

        private void readButtonData(HidDeviceData InData)
        {
            timerHibernation.Enabled = false;

            if ((InData.Status == HidDeviceData.ReadStatus.Success) && (InData.Data[0] == 1))
            {
                Log.Debug("Read button data: " + String.Join(",", InData.Data));

                if ((InData.Data[10] == 0) || (InData.Data[4] == 255)) // button released
                {
                    if (ButtonReleased != null && isButtonDown) ButtonReleased(this, new ButtonData(lastButton));
                }
                else // button pressed
                {
	                var bCode = new ButtonCode(InData.Data, 1);
	                if (buttonCodes.ContainsKey(bCode))
	                {
						lastButton = buttonCodes[bCode];
						isButtonDown = true;
						if (ButtonDown != null) ButtonDown(this, new ButtonData(lastButton));
					}
                }

                byte batteryReading = (byte)(InData.Data[11] * 20);

                if (batteryReading != _batteryLife) //Check battery life reading.
                {
                    _batteryLife = batteryReading;

                    if (BatteryLifeChanged != null) BatteryLifeChanged(this, new EventArgs());
                }
                
                if (_hibernationEnabled) timerHibernation.Enabled = true;

                hidRemote.Read(readButtonData); //Read next button pressed.

                return;
            }

            Log.Debug("Read remote data: " + String.Join(",", InData.Data));

            if (Disconnected != null) Disconnected(this, new EventArgs());

            hidRemote.Dispose(); //Dispose of current remote.

            hidRemote = null;

            timerFindRemote.Enabled = true; //Try to reconnect.
        }

        private void timerFindRemote_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hidRemote == null)
            {
                Log.Debug("Searching for remote");

                IEnumerator<HidDevice> devices = HidDevices.Enumerate(vendorId, productId).GetEnumerator();
                
                if (devices.MoveNext()) hidRemote = devices.Current;

                if (hidRemote != null)
                {
                    Log.Debug("Remote found");

                    hidRemote.OpenDevice();

                    if (Connected != null)
                    {
                        Connected(this, new EventArgs());
                    }

                    hidRemote.Read(readButtonData);

                    timerFindRemote.Enabled = false;
                }
            }
            else
            {
                timerFindRemote.Enabled = false;
            }
        }

        private void timerHibernation_Elapsed(object sender, ElapsedEventArgs e)
        {
            /*if (Log.isLogging) Log.Debug("Attempting to hibernate remote");

            try
            {
                HardwareAPI.DisableDevice(n => n.ToUpperInvariant().Contains
                    ("_VID&0002054C_PID&0306"), true);

                System.Threading.Thread.Sleep(1500);

                HardwareAPI.DisableDevice(n => n.ToUpperInvariant().Contains
                    ("_VID&0002054C_PID&0306"), false);
            }
            catch (Exception ex)
            {
                if (Log.isLogging) Log.Debug("Unable to hibernate remote:" + ex.Message);
            }

            timerFindRemote.Enabled = true;*/
            timerHibernation.Enabled = false;
        }

        public class ButtonData : EventArgs
        {
            public ButtonType button;

            public ButtonData(ButtonType btn)
            {
                button = btn;
            }
        }

	    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	}
}
