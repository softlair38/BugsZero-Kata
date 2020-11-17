namespace Trivia.Domain.Events
{
	public record PlayerRollRequested : IDomainRequest
	{
		public Game Game { get; init; }
		public Player Player { get; init; }

		internal PlayerRollRequested(Game game, Player player)
			=> (Game, Player) = (game, player);

		public void Response(Roll roll)
		{
			Player.Roll(roll);
		}
	}
}
