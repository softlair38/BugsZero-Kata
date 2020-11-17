namespace Trivia.Domain.Events
{
	public record PlayerStayedInPenaltyBox : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerStayedInPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
