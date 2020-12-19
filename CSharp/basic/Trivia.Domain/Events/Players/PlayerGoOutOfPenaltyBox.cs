using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoOutOfPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
