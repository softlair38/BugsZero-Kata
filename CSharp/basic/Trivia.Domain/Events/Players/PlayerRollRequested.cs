namespace Trivia.Domain.Events
{
	public record PlayerRollRequested : IDomainRequest
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerRollRequested(Game game, Player player)
			=> (Game, Player) = (game, player);

		public void Response(Roll roll)
		{
			Player.Roll(roll);
		}
	}
}
