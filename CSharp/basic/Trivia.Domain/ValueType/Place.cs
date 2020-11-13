namespace Trivia
{
	public readonly struct Place
	{
		public Category Category { get; }

		public Location Location { get; }

		public Place(Category category, Location location)
		{
			Category = category;
			Location = location;
		}
	}
}
