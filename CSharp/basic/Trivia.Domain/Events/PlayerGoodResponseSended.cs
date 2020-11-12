namespace Trivia.Domain.Events
{
	public readonly struct PlayerGoodResponseSended : IDomainEvent
	{
		public Game Game { get; }

		internal PlayerGoodResponseSended(Game game)
		{
			Game = game;
		}
	}
}