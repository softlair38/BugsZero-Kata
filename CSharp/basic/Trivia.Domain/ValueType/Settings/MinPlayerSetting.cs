using System;

namespace Trivia.Domain.ValueType.Settings
{
	public record MinPlayerSetting
	{
		internal ushort Value { get; }

		public MinPlayerSetting(ushort value)
		{
			if (value < 2)
				throw new ArgumentOutOfRangeException("min 2 players");

			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
