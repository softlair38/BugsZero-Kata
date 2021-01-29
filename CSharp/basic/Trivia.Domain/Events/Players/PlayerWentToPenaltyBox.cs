using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerWentToPenaltyBox : IDomainEvent
	{
		public Player Player { get; }

		internal PlayerWentToPenaltyBox(Player player)
			=> Player = player;
	}
}
