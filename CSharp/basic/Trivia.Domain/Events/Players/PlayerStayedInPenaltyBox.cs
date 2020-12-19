using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerStayedInPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerStayedInPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
