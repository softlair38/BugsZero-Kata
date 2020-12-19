namespace Trivia.Domain.ValueType
{
	public record Question(string Description, Response GoodResponseExpected)
	{
		internal bool IsGoodResponse(Response response) => response.Equals(GoodResponseExpected);

		public override string ToString()
		{
			return Description;
		}
	}
}
