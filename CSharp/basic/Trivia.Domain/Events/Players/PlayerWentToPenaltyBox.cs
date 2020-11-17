namespace Trivia.Domain.Events
{
	public record PlayerWentToPenaltyBox : IDomainEvent
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerWentToPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
