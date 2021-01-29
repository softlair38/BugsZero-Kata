using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Games
{
	public record GameStarted : IDomainEvent
	{
		public Game Game { get; }

		internal GameStarted(Game game)
			=> Game = game;
	}
}
