namespace Trivia
{
	public readonly struct Response
	{
		internal int Description { get; }

		public Response(int description)
		{
			Description = description;
		}
	}
}