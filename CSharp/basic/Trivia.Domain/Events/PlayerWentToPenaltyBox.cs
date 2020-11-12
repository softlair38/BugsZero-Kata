namespace Trivia.Domain.Events
{
	public readonly struct PlayerWentToPenaltyBox : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerWentToPenaltyBox(Game game, Player player)
		{
			Game = game;
			Player = player;
		}
	}
}
