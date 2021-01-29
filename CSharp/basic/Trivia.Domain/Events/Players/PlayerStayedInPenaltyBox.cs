using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerStayedInPenaltyBox : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerStayedInPenaltyBox(Player player)
			=> Player = player;
	}
}
