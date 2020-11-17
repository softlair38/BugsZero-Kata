namespace Trivia.Domain.Events
{
	public record PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerGoOutOfPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
