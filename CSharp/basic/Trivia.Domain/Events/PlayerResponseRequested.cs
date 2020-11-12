namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseRequested : IDomainRequest
	{
		internal PlayerResponseRequested(Game game, Player player, string question, string category)
		{
			Game = game;
			Player = player;
			Question = question;
			Category = category;
		}

		private Game Game { get; }
		public Player Player { get; }
		public string Question { get; }
		public string Category { get; }

		public void Response(PlayerResponseResponse playerResponseResponse)
		{
			if (playerResponseResponse.Response == 7)
				Game.WrongAnswer();
			else
				Game.WasCorrectlyAnswered();
		}
	}
}
