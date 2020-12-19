namespace Trivia.Domain.ValueType
{
	public record Location(int Number)
	{
		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
