using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerGoOutOfPenaltyBox(Player player)
			=> Player = player;
	}
}
