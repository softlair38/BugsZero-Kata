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

			Game.StartNewGame(new Player("Chet"), new Player("Pat"), new Player("Sue"));

			Domains.OnDomainTriggered -= OnDomainTriggered;
		}

		private static int _r2;

		private static void OnDomainTriggered(IDomainBase domainEvent)
		{
			switch (domainEvent)
			{
				case PlayerRollRequested playerRollRequested:
					int r1 = Rand.Next(5) + 1;
					_r2 = Rand.Next(9);
					playerRollRequested.Response(new PlayerRollResponse(new Roll(r1)));
					break;

				case PlayerResponseRequested playerResponseRequested:
					playerResponseRequested.Response(new PlayerResponseResponse(_r2));
					break;
			}
		}
	}
}
