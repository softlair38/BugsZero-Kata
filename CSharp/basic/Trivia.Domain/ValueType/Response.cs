namespace Trivia
{
	public readonly struct Response
	{
		private int Description { get; }

		public Response(int description)
		{
			Description = description;
		}

		public override string ToString()
		{
			return Description.ToString();
		}
	}
}
