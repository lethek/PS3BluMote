using System;

namespace PS3BluMote
{
	public class RemoteStateData : EventArgs
	{
		public readonly byte? BatteryLife;
		public readonly bool IsConnected;

		public RemoteStateData(byte? batteryLife, bool isConnected)
		{
			BatteryLife = batteryLife;
			IsConnected = isConnected;
		}
	}
}