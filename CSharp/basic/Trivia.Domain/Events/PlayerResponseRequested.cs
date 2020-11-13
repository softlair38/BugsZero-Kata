namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseRequested : IDomainRequest
	{
		internal PlayerResponseRequested(Game game, Player player, string question)
		{
			Game = game;
			Player = player;
			Question = question;
		}

		private Game Game { get; }
		public Player Player { get; }
		public string Question { get; }

		public void Response(PlayerResponseResponse playerResponseResponse)
		{
			if (playerResponseResponse.Response == 7)
				Player.WrongAnswer();
			else
				Player.WasCorrectlyAnswered();
		}
	}
}
