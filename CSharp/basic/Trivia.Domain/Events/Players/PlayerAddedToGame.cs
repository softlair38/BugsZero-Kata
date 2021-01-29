using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerAddedToGame : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerAddedToGame(Player player)
			=> Player = player;
	}
}
