using Trivia.Domain.Events.Base;
using Trivia.Domain.ValueType;

namespace Trivia.Domain.Events.Players
{
	public record PlayerRollRequested : IDomainRequest
	{
		public Game Game { get; }
		public Player Player { get; }

		internal PlayerRollRequested(Game game, Player player)
			=> (Game, Player) = (game, player);

		public void Response(Roll roll)
		{
			Player.Roll(roll);
		}
	}
}
