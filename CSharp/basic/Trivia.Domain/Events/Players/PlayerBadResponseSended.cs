namespace Trivia.Domain.Events
{
	public record PlayerBadResponseSended : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerBadResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
