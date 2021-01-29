using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Games
{
	public record GameEnded : IDomainEvent
	{
		public Game Game { get; }

		internal GameEnded(Game game)
			=> Game = game;
	}
}
