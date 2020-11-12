namespace Trivia.Domain.Events
{
	public readonly struct GameEnded : IDomainEvent
	{
		public Game Game { get; }

		internal GameEnded(Game game)
		{
			Game = game;
		}
	}
}
