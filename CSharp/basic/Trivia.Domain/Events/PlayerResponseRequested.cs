namespace Trivia.Domain.Events
{
	public struct PlayerResponseRequested : IDomainEvent
	{
		internal PlayerResponseRequested(Game game)
		{
			Game = game;
		}

		public Game Game { get; }
	}
}
