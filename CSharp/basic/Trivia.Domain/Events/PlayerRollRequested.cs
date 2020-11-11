namespace Trivia.Domain.Events
{
	public struct PlayerRollRequested : IDomainEvent
	{
		internal PlayerRollRequested(Game game)
		{
			Game = game;
		}

		public Game Game { get; }
	}
}
