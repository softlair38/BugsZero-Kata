namespace Trivia
{
	public readonly struct Question
	{
		private string Description { get; }

		private Response GoodResponseExpected { get; }

		internal bool IsGoodResponse(Response response) => response.Description == GoodResponseExpected.Description;

		public Question(string description, Response goodResponseExpected)
		{
			Description = description;
			GoodResponseExpected = goodResponseExpected;
		}

		public override string ToString()
		{
			return Description;
		}
	}
}
