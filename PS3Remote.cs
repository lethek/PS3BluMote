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
using System.Linq;
using System.Timers;
using HidLibrary;
using NLog;
using Stateless;

using Timer = System.Timers.Timer;


namespace PS3BluMote
{

	public class PS3Remote
	{

		public event EventHandler<EventArgs> BatteryLifeChanged;
		public event EventHandler<ButtonData> ButtonDown;
		public event EventHandler<EventArgs> ButtonReleased;
		public event EventHandler<EventArgs> Connected;
		public event EventHandler<EventArgs> Disconnected;

		public byte? BatteryLife { get; private set; }
		public string BatteryLifeString { get { return BatteryLife.HasValue ? BatteryLife + "%" : "Unknown"; } }
		public bool IsConnected { get { return _timerFindRemote.Enabled; } }


		public PS3Remote(int vendor, int product)
		{
			_vendorId = vendor;
			_productId = product;

			_buttonDownTrigger = _stateMachine.SetTriggerParameters<ButtonData>(RemoteTrigger.ButtonDown);

			_stateMachine.Configure(RemoteState.WaitingForButtonDown)
				.OnEntry(StopSendingKeys)
				.Ignore(RemoteTrigger.ButtonUp)
				.Permit(RemoteTrigger.ButtonDown, RemoteState.WaitingForButtonUp)
				.Permit(RemoteTrigger.Exit, RemoteState.Exiting);

			_stateMachine.Configure(RemoteState.WaitingForButtonUp)
				.OnEntryFrom(_buttonDownTrigger, StartSendingKeys)
				.Ignore(RemoteTrigger.ButtonDown)
				.Permit(RemoteTrigger.ButtonUp, RemoteState.WaitingForButtonDown)
				.Permit(RemoteTrigger.Exit, RemoteState.Exiting);

			_stateMachine.Configure(RemoteState.Exiting)
				.OnEntry(StartExiting)
				.Ignore(RemoteTrigger.ButtonDown)
				.Ignore(RemoteTrigger.ButtonUp)
				.Ignore(RemoteTrigger.Exit);


			_timerFindRemote = new Timer { Interval = 1500 };
			_timerFindRemote.Elapsed += timerFindRemote_Elapsed;
		}


		public void Connect()
		{
			_timerFindRemote.Enabled = true;
		}




		private void OnRead(HidDeviceData deviceOutput)
		{
			//Check whether the read was successful or not
			if (deviceOutput.Status != HidDeviceData.ReadStatus.Success || deviceOutput.Data[0] != 1) {
				Log.Trace("Read remote data: " + String.Join(",", deviceOutput.Data));
				Log.Warn("Did not successfully read button data. Attempting to reconnect...");

				var disconnectedEvent = Disconnected;
				if (disconnectedEvent != null) {
					disconnectedEvent(this, new EventArgs());
				}
				_hidRemote.Dispose();
				_hidRemote = null;

				//Try to reconnect
				_timerFindRemote.Enabled = true;
				return;
			}

			//Read button data and adjust state
			Log.Trace(() => "Read button data: " + String.Join(",", deviceOutput.Data));
			if (deviceOutput.Data[10] == 0 || deviceOutput.Data[4] == 255) {
				_stateMachine.Fire(RemoteTrigger.ButtonUp);
			} else {
				var buttonCode = new ButtonCode(deviceOutput.Data, 1);
				if (Buttons.ContainsKey(buttonCode)) {
					var buttonData = new ButtonData(Buttons[buttonCode]);
					_stateMachine.Fire(_buttonDownTrigger, buttonData);
				}
			}

			//Read battery data
			var batteryReading = (byte)(deviceOutput.Data[11] * 20);
			if (BatteryLife != batteryReading) {
				BatteryLife = batteryReading;
				var batteryLifeChangedEvent = BatteryLifeChanged;
				if (batteryLifeChangedEvent != null) {
					batteryLifeChangedEvent(this, new EventArgs());
				}
			}

			//Queue up another async read from the device
			_hidRemote.Read(OnRead);
		}


		private void StartSendingKeys(ButtonData buttonData)
		{
			var buttonDownEvent = ButtonDown;
			if (buttonDownEvent != null) {
				buttonDownEvent(this, buttonData);
			}
		}


		private void StopSendingKeys()
		{
			var buttonReleasedEvent = ButtonReleased;
			if (buttonReleasedEvent != null) {
				buttonReleasedEvent(this, new EventArgs());
			}
		}


		private void StartExiting()
		{
			//TODO: use cancellation token or something to signal the send thread? maybe this state isn't even necessary
		}


		private void timerFindRemote_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (_hidRemote == null) {
				Log.Debug("Searching for remote");
				_hidRemote = HidDevices.Enumerate(_vendorId, _productId).FirstOrDefault();

				if (_hidRemote != null) {
					Log.Debug("Remote found");
					_hidRemote.OpenDevice();
					var connectedEvent = Connected;
					if (connectedEvent != null) {
						connectedEvent(this, new EventArgs());
					}
					_timerFindRemote.Enabled = false;
					_hidRemote.Read(OnRead);
				}

			} else {
				_timerFindRemote.Enabled = false;
			}
		}


		public class ButtonData : EventArgs
		{
			public readonly ButtonType Button;
			public readonly DateTime Time;

			public ButtonData(ButtonType btn)
			{
				Button = btn;
				Time = DateTime.Now;
			}
		}


		private enum RemoteState
		{
			WaitingForButtonDown,
			WaitingForButtonUp,
			Exiting
		}


		private enum RemoteTrigger
		{
			ButtonDown,
			ButtonUp,
			Exit
		}


		private HidDevice _hidRemote;
		private readonly Timer _timerFindRemote;
		private readonly int _vendorId = 0x054c;
		private readonly int _productId = 0x0306;

		private readonly StateMachine<RemoteState, RemoteTrigger> _stateMachine = new StateMachine<RemoteState, RemoteTrigger>(RemoteState.WaitingForButtonDown);
		private readonly StateMachine<RemoteState, RemoteTrigger>.TriggerWithParameters<ButtonData> _buttonDownTrigger;

		private static readonly Dictionary<ButtonCode, ButtonType> Buttons = new Dictionary<ButtonCode, ButtonType>() {
	        { new ButtonCode(0, 0, 0, 22), ButtonType.Eject },
	        { new ButtonCode(0, 0, 0, 100), ButtonType.Audio },
	        { new ButtonCode(0, 0, 0, 101), ButtonType.Angle },
	        { new ButtonCode(0, 0, 0, 99), ButtonType.Subtitle },
	        { new ButtonCode(0, 0, 0, 15), ButtonType.Clear },
	        { new ButtonCode(0, 0, 0, 40), ButtonType.Time },
	        { new ButtonCode(0, 0, 0, 0), ButtonType.NUM_1 },
	        { new ButtonCode(0, 0, 0, 1), ButtonType.NUM_2 },
	        { new ButtonCode(0, 0, 0, 2), ButtonType.NUM_3 },
	        { new ButtonCode(0, 0, 0, 3), ButtonType.NUM_4 },
	        { new ButtonCode(0, 0, 0, 4), ButtonType.NUM_5 },
	        { new ButtonCode(0, 0, 0, 5), ButtonType.NUM_6 },
	        { new ButtonCode(0, 0, 0, 6), ButtonType.NUM_7 },
	        { new ButtonCode(0, 0, 0, 7), ButtonType.NUM_8 },
	        { new ButtonCode(0, 0, 0, 8), ButtonType.NUM_9 },
	        { new ButtonCode(0, 0, 0, 9), ButtonType.NUM_0 },
	        { new ButtonCode(0, 0, 0, 128), ButtonType.Blue },
	        { new ButtonCode(0, 0, 0, 129), ButtonType.Red },
	        { new ButtonCode(0, 0, 0, 130), ButtonType.Green },
	        { new ButtonCode(0, 0, 0, 131), ButtonType.Yellow },
	        { new ButtonCode(0, 0, 0, 112), ButtonType.Display },
	        { new ButtonCode(0, 0, 0, 26), ButtonType.Top_Menu },
	        { new ButtonCode(0, 0, 0, 64), ButtonType.PopUp_Menu },
	        { new ButtonCode(0, 0, 0, 14), ButtonType.Return },
	        { new ButtonCode(0, 16, 0, 92), ButtonType.Triangle },
	        { new ButtonCode(0, 32, 0, 93), ButtonType.Circle },
	        { new ButtonCode(0, 128, 0, 95), ButtonType.Square },
	        { new ButtonCode(0, 64, 0, 94), ButtonType.Cross },
	        { new ButtonCode(16, 0, 0, 84), ButtonType.Arrow_Up },
	        { new ButtonCode(64, 0, 0, 86), ButtonType.Arrow_Down },
	        { new ButtonCode(128, 0, 0, 87), ButtonType.Arrow_Left },
	        { new ButtonCode(32, 0, 0, 85), ButtonType.Arrow_Right },
	        { new ButtonCode(0, 0, 8, 11), ButtonType.Enter },
	        { new ButtonCode(0, 4, 0, 90), ButtonType.L1 },
	        { new ButtonCode(0, 1, 0, 88), ButtonType.L2 },
	        { new ButtonCode(2, 0, 0, 81), ButtonType.L3 },
	        { new ButtonCode(0, 8, 0, 91), ButtonType.R1 },
	        { new ButtonCode(0, 2, 0, 89), ButtonType.R2 },
	        { new ButtonCode(4, 0, 0, 82), ButtonType.R3 },
	        { new ButtonCode(0, 0, 1, 67), ButtonType.Playstation },
	        { new ButtonCode(1, 0, 0, 80), ButtonType.Select },
	        { new ButtonCode(8, 0, 0, 83), ButtonType.Start },
	        { new ButtonCode(0, 0, 0, 50), ButtonType.Play },
	        { new ButtonCode(0, 0, 0, 56), ButtonType.Stop },
	        { new ButtonCode(0, 0, 0, 57), ButtonType.Pause },
	        { new ButtonCode(0, 0, 0, 51), ButtonType.Scan_Back },
	        { new ButtonCode(0, 0, 0, 52), ButtonType.Scan_Forward },
	        { new ButtonCode(0, 0, 0, 48), ButtonType.Prev },
	        { new ButtonCode(0, 0, 0, 49), ButtonType.Next },
	        { new ButtonCode(0, 0, 0, 96), ButtonType.Step_Back },
	        { new ButtonCode(0, 0, 0, 97), ButtonType.Step_Forward },
            { new ButtonCode(0, 0, 0, 118), ButtonType.Instant_Back },
            { new ButtonCode(0, 0, 0, 117), ButtonType.Instant_Forward },
            { new ButtonCode(0, 0, 0, 16), ButtonType.Channel_Up },
            { new ButtonCode(0, 0, 0, 17), ButtonType.Channel_Down },
            { new ButtonCode(0, 0, 0, 12), ButtonType.dash_slash_dash_dash }
	    };

		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	}

}
