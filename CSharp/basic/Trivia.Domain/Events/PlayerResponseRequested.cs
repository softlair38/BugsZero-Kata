namespace Trivia.Domain.Events
{
	public struct PlayerResponseRequested : IDomainEvent
	{
		internal PlayerResponseRequested(Game game, int randomAnswer)
		{
			Game = game;
			RandomAnswer = randomAnswer;
		}

		public Game Game { get; }
		public int RandomAnswer { get; }
	}
}
