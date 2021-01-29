using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerBadResponseSended : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerBadResponseSended(Player player)
			=> Player = player;
	}
}
