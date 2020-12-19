namespace Trivia.Domain.Events
{
	public record PlayerGoodResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoodResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
