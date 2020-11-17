using System;

namespace Trivia
{
	public record NbCoinToWinSetting
	{
		internal ushort Value { get; }

		public NbCoinToWinSetting(ushort value)
		{
			if (value < 2)
				throw new ArgumentOutOfRangeException("min 2 coin to win");

			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
