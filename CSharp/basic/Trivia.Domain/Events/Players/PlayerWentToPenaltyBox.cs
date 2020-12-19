namespace Trivia.Domain.Events
{
	public record PlayerWentToPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerWentToPenaltyBox(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
