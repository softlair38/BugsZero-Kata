namespace Trivia
{
	public readonly struct Roll
	{
		internal int Number { get; }
		internal bool GoodAnswer { get; }

		public Roll(int nb, bool goodAnswer)
		{
			Number = nb;
			GoodAnswer = goodAnswer;
		}

		internal bool IsGettingOutOfPenaltyBox => Number % 2 != 0;

		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
