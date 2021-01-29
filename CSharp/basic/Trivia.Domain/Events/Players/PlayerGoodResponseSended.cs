using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerGoodResponseSended : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerGoodResponseSended(Player player)
			=> Player = player;
	}
}
