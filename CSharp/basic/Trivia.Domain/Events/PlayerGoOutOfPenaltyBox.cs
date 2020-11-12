namespace Trivia.Domain.Events
{
	public readonly struct PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerGoOutOfPenaltyBox(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
