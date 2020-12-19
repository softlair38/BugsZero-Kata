namespace Trivia.Domain.ValueType
{
	public record Roll(int Number)
	{
		internal bool IsGettingOutOfPenaltyBox => Number % 2 != 0;

		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
