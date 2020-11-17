namespace Trivia
{
	public record Response(int Description)
	{
		public override string ToString()
		{
			return Description.ToString();
		}
	}
}
