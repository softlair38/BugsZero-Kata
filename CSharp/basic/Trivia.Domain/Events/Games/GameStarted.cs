namespace Trivia.Domain.Events
{
	public record GameStarted : IDomainEvent
	{
		public Game Game { get; init; }


		internal GameStarted(Game game)
			=> (Game) = (game);
	}
}
