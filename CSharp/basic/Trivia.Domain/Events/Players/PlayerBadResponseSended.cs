using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerBadResponseSended : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerBadResponseSended(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
