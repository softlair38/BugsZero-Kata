namespace Trivia
{
	public struct Roll
	{
		internal int Number { get; }

		public Roll(int nb)
		{
			Number = nb;
		}

		internal bool IsGettingOutOfPenaltyBox => Number % 2 != 0;
	}
}
