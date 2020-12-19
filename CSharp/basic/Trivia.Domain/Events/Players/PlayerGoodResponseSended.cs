using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerGoodResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoodResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
