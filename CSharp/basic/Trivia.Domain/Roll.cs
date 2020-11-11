namespace Trivia
{
	public readonly struct Roll
	{
		internal int Number { get; }
		public int RandomAnswer { get; }

		public Roll(int nb, int randomAnswer)
		{
			Number = nb;
			RandomAnswer = randomAnswer;
		}

		internal bool IsGettingOutOfPenaltyBox => Number % 2 != 0;

		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
