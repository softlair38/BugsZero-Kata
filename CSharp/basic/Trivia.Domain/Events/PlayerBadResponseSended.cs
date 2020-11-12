namespace Trivia.Domain.Events
{
	public readonly struct PlayerBadResponseSended : IDomainEvent
	{
		public Game Game { get; }

		internal PlayerBadResponseSended(Game game)
		{
			Game = game;
		}
	}
}