namespace Trivia
{
	public readonly struct Location
	{
		public int Number { get; }

		public Location(int number)
		{
			Number = number;
		}

		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
