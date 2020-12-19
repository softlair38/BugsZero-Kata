using System;

namespace Trivia.Domain.ValueType.Settings
{
	public record MaxPlayerSetting
	{
		internal ushort Value { get; }

		public MaxPlayerSetting(ushort value)
		{
			if (value > 50)
				throw new ArgumentOutOfRangeException("max 50 players");

			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
