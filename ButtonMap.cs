using System;

namespace PS3BluMote
{

	public class ButtonMap
	{

		public byte this[int index]
		{
			get { return _data[index]; }
		}

		public ButtonMap(byte[] data)
		{
			if (data.Length != 4) {
				throw new ArgumentException("Argument must be a 4 byte array", "data");
			}
			data.CopyTo(_data, 0);
		}

		public ButtonMap(byte byte0, byte byte1, byte byte2, byte byte3)
			: this(new byte[] { byte0, byte1, byte2, byte3 })
		{
		}

		public override string ToString()
		{
			return String.Join(",", _data);
		}

		private byte[] _data = new byte[4];

	}

}
