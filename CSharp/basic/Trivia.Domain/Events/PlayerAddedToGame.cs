namespace Trivia.Domain.Events
{
	public readonly struct PlayerAddedToGame : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }
		public int NumPlayer { get; }

		internal PlayerAddedToGame(Game game, Player player, int numPlayer)
		{
			Game = game;
			Player = player;
			NumPlayer = numPlayer;
		}
	}
}
