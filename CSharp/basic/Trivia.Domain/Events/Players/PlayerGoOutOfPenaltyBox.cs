namespace Trivia.Domain.Events
{
	public record PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoOutOfPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
