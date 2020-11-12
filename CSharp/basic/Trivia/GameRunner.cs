using System;
using Trivia.Domain.Events;

namespace Trivia
{
	public static class GameRunner
	{
		private static readonly Random Rand = new Random(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		public static void Main(string[] args)
		{
			Domains.OnDomainTriggered += OnDomainTriggered;

			var game = new Game(new Player("Chet"), new Player("Pat"), new Player("Sue"));
			RunOnce(game);

			Domains.OnDomainTriggered -= OnDomainTriggered;
		}

		private static void RunOnce(Game game)
		{
			var roll = new Roll(Rand.Next(5) + 1, Rand.Next(9) == 7);
			game.Roll(roll);
		}

		private static void OnDomainTriggered(IDomainBase domainEvent)
		{
			switch (domainEvent)
			{
				case PlayerRollRequested playerRollRequested:
					playerRollRequested.Response(new PlayerRollResponse(new Roll(Rand.Next(5) + 1, Rand.Next(9) == 7), playerRollRequested));
					break;
			}
		}
	}
}
