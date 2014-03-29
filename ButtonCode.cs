using System;

namespace PS3BluMote
{

	public class ButtonCode
	{
		public byte this[int index] { get { return _bytes[index]; } }

		public int Length { get { return 4; } }

		public ButtonCode(uint value)
		{
			_value = value;
			_bytes = BitConverter.GetBytes(_value);
		}

		public ButtonCode(byte[] bytes, int index = 0)
			: this(BitConverter.ToUInt32(bytes, index)) { }

		public ButtonCode(byte byte0, byte byte1, byte byte2, byte byte3)
			: this(new[] { byte0, byte1, byte2, byte3 }, 0) { }

		public override string ToString()
		{
			return String.Join(",", _value);
		}

		public override int GetHashCode()
		{
			return (int)_value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ButtonCode)obj);
		}

		protected bool Equals(ButtonCode other)
		{
			if (other == null) {
				return false;
			}
			return _value == other._value;
		}

		private readonly uint _value;
		private readonly byte[] _bytes;
	}

}
