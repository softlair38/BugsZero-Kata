namespace Trivia.Domain.Events
{
	public record GameEnded : IDomainEvent
	{
		public Game Game { get; }

		internal GameEnded(Game game)
			=> (Game) = (game);
	}
}
