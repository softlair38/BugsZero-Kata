using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Players
{
	public record PlayerAddedToGame : IDomainEvent
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerAddedToGame(Game game, Player player)
			=> (Game, Player) = (game, player);
	}
}
