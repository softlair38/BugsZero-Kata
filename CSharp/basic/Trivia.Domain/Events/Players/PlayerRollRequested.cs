using Trivia.Domain.Events.Base;
using Trivia.Domain.ValueType;

namespace Trivia.Domain.Events.Players
{
	public record PlayerRollRequested : IDomainRequest
	{
		public Player Player { get; }

		internal PlayerRollRequested(Player player)
			=> Player = player;

		public void Response(Roll roll)
		{
			Player.Roll(roll);
		}
	}
}
