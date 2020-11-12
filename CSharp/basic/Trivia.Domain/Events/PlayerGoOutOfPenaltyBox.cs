namespace Trivia.Domain.Events
{
	public readonly struct PlayerGoOutOfPenaltyBox : IDomainEvent
	{
		public Game Game { get; }

		internal PlayerGoOutOfPenaltyBox(Game game)
		{
			Game = game;
		}
	}
}
