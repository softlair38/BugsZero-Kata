namespace Trivia.Domain.Events
{
	public readonly struct PlayerWentToPenaltyBox : IDomainEvent
	{
		public Game Game { get; }

		internal PlayerWentToPenaltyBox(Game game)
		{
			Game = game;
		}
	}
}