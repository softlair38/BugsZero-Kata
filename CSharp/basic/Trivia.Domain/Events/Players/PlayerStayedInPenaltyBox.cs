namespace Trivia.Domain.Events
{
	public readonly struct PlayerStayedInPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerStayedInPenaltyBox(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
