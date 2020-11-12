namespace Trivia.Domain.Events
{
	public readonly struct PlayerRollRequested : IDomainRequest
	{
		internal PlayerRollRequested(Game game)
		{
			Game = game;
		}

		private Game Game { get; }

		public void Response(PlayerRollResponse playerRollResponse)
		{
			Game.Roll(playerRollResponse.Roll);
		}
	}
}
