using System;
using Trivia.Domain.Events;

namespace Trivia
{
	public static class GameRunner
	{
		private static readonly Random Rand = new Random(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		public static void Main(string[] args)
		{
			DomainEvent.OnDomainEventTriggered += OnDomainEventTriggered;

			var game = new Game(new Player("Chet"), new Player("Pat"), new Player("Sue"));
			RunOnce(game);

			DomainEvent.OnDomainEventTriggered -= OnDomainEventTriggered;
		}

		private static void RunOnce(Game game)
		{
			var roll = new Roll(Rand.Next(5) + 1, Rand.Next(9));
			game.Roll(roll);
		}

		private static void OnDomainEventTriggered(IDomainEvent domainEvent)
		{
			switch (domainEvent)
			{
				case PlayerRollRequested playerRollRequested:
					RunOnce(playerRollRequested.Game);
					break;

				case PlayerResponseRequested playerResponseRequested:
					if (playerResponseRequested.RandomAnswer == 7)
						playerResponseRequested.Game.WrongAnswer();
					else
						playerResponseRequested.Game.WasCorrectlyAnswered();
					break;
			}
		}
	}
}
