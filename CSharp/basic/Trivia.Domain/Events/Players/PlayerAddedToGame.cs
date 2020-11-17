namespace Trivia.Domain.Events
{
	public record PlayerAddedToGame : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerAddedToGame(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
