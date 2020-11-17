namespace Trivia.Domain.Events
{
	public record PlayerGoodResponseSended : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerGoodResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
