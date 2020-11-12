namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseRequested : IDomainRequest
	{
		internal PlayerResponseRequested(Game game)
		{
			Game = game;
		}

		private Game Game { get; }

		public void Response(PlayerResponseResponse playerResponseResponse)
		{
			if (playerResponseResponse.Response == 7)
				Game.WrongAnswer();
			else
				Game.WasCorrectlyAnswered();
		}
	}
}
