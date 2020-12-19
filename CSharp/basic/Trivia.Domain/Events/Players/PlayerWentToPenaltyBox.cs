using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerWentToPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerWentToPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
