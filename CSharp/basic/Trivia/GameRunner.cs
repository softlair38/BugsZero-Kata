using System;
using Trivia.Domain.Events;

namespace Trivia
{
	public class GameRunner : IDisposable
	{
		private static readonly Random Rand = new Random(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		public static void Main(string[] args)
		{
			using (var gameRunner = new GameRunner())
			{
				gameRunner.Launch();
			}
		}

		private GameRunner()
		{
			Domains.OnDomainTriggered += OnDomainTriggered;
		}

		private void Launch()
		{
			Game.StartNewGame(new Player("Chet"), new Player("Pat"), new Player("Sue"));
		}

		private int _r2;

		private void OnDomainTriggered(IDomainBase domainEvent)
		{
			switch (domainEvent)
			{
				case PlayerAddedToGame playerAddedToGame:
					Console.WriteLine($"{playerAddedToGame.Player} was Added");
					Console.WriteLine($"They are player number {playerAddedToGame.NumPlayer}");
					break;

				case PlayerRollRequested playerRollRequested:
					int r1 = Rand.Next(5) + 1;
					_r2 = Rand.Next(9);
					Console.WriteLine($"{playerRollRequested.Player} is the current player");
					Console.WriteLine($"They have rolled a {r1}");
					playerRollRequested.Response(new PlayerRollResponse(new Roll(r1)));
					break;

				case PlayerResponseRequested playerResponseRequested:
					Console.WriteLine($"{playerResponseRequested.Player}'s new location is {playerResponseRequested.Player.Place}");
					Console.WriteLine($"The category is {playerResponseRequested.Category}");
					Console.WriteLine(playerResponseRequested.Question);
					playerResponseRequested.Response(new PlayerResponseResponse(_r2));
					break;

				case PlayerWentToPenaltyBox playerWentToPenaltyBox:
					Console.WriteLine($"{playerWentToPenaltyBox.Player} was sent to the penalty box");
					break;

				case PlayerStayedInPenaltyBox playerStayedInPenaltyBox:
					Console.WriteLine($"{playerStayedInPenaltyBox.Player} is not getting out of the penalty box");
					break;

				case PlayerGoOutOfPenaltyBox playerGoOutOfPenaltyBox:
					Console.WriteLine($"{playerGoOutOfPenaltyBox.Player} is getting out of the penalty box");
					break;

				case PlayerBadResponseSended playerBadResponseSended:
					Console.WriteLine("Question was incorrectly answered");
					break;

				case PlayerGoodResponseSended playerGoodResponseSended:
					Console.WriteLine("Answer was correct!!!!");
					Console.WriteLine($"{playerGoodResponseSended.Player} now has {playerGoodResponseSended.Player.Purses} Gold Coins.");
					break;
			}
		}

		public void Dispose()
		{
			Domains.OnDomainTriggered -= OnDomainTriggered;
		}
	}
}
