namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseRequested : IDomainRequest
	{
		public Game Game { get; }
		public Player Player { get; }
		public Question Question { get; }

		internal PlayerResponseRequested(Game game, Player player, Question question)
		{
			Game = game;
			Player = player;
			Question = question;
		}

		public void Response(Response response)
		{
			if (Question.IsGoodResponse(response))
				Player.WrongAnswer();
			else
				Player.WasCorrectlyAnswered();
		}
	}
}
