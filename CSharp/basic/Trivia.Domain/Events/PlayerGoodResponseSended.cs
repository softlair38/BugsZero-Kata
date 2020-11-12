namespace Trivia.Domain.Events
{
	public readonly struct PlayerGoodResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoodResponseSended(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
