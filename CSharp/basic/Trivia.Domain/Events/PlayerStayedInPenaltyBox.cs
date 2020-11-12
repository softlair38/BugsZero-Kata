namespace Trivia.Domain.Events
{
	public readonly struct PlayerStayedInPenaltyBox : IDomainEvent
	{
		public Game Game { get; }

		internal PlayerStayedInPenaltyBox(Game game)
		{
			Game = game;
		}
	}
}