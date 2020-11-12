namespace Trivia.Domain.Events
{
	public readonly struct PlayerRollRequested : IDomainRequest
	{
		internal PlayerRollRequested(Game game, Player player)
		{
			Game = game;
			Player = player;
		}

		private Game Game { get; }
		public Player Player { get; }

		public void Response(PlayerRollResponse playerRollResponse)
		{
			Player.Roll(playerRollResponse.Roll);
		}
	}
}
