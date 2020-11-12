namespace Trivia.Domain.Events
{
	public readonly struct GameStarted : IDomainEvent
	{
		public Game Game { get; }

		internal GameStarted(Game game)
		{
			Game = game;
		}
	}
}