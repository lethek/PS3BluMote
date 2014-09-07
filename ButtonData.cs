using System;

namespace PS3BluMote
{

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

}
