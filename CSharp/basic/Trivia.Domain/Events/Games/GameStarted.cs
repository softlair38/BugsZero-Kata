namespace Trivia.Domain.Events
{
	public record GameStarted : IDomainEvent
	{
		public Game Game { get; }


		internal GameStarted(Game game)
			=> (Game) = (game);
	}
}
