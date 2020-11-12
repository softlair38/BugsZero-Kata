namespace Trivia.Domain.Events
{
	public readonly struct PlayerBadResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerBadResponseSended(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
