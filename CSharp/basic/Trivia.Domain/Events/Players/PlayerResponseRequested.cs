namespace Trivia.Domain.Events
{
	public record PlayerResponseRequested : IDomainRequest
	{
		public Game Game { get; }
		public Player Player { get; }
		public Question Question { get; }

		internal PlayerResponseRequested(Game game, Player player, Question question)
			=> (Game, Player, Question) = (game, player, question);

		public void Response(Response response)
		{
			if (Question.IsGoodResponse(response))
				Player.WrongAnswer();
			else
				Player.WasCorrectlyAnswered();
		}
	}
}
