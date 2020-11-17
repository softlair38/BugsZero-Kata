using System;

namespace Trivia
{
	public readonly struct NbPlacesSetting
	{
		internal ushort Value { get; }

		public NbPlacesSetting(ushort value)
		{
			if (value < 2)
				throw new ArgumentOutOfRangeException("min 2 places");

			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
