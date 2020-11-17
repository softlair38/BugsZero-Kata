namespace Trivia.Domain.Events
{
	public readonly struct PlayerAddedToGame : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerAddedToGame(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
