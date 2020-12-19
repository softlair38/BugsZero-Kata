namespace Trivia.Domain.Events
{
	public record PlayerBadResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerBadResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
