namespace Trivia
{
	public readonly struct Place
	{
		public Category Category { get; }

		public int Location { get; }

		public Place(Category category, int location)
		{
			Category = category;
			Location = location;
		}
	}
}
