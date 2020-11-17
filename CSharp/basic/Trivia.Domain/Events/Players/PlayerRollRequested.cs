namespace Trivia.Domain.Events
{
	public readonly struct PlayerRollRequested : IDomainRequest
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerRollRequested(Game game, Player player)
		{
			Game = game;
			Player = player;
		}

		public void Response(Roll roll)
		{
			Player.Roll(roll);
		}
	}
}
