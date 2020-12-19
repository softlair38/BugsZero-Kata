﻿using System;
using Trivia.Domain;
using Trivia.Domain.Events.Base;
using Trivia.Domain.Events.Games;
using Trivia.Domain.Events.Players;
using Trivia.Domain.ValueType;
using Trivia.Domain.ValueType.Settings;

namespace Trivia
{
	public class GameRunner : IDisposable
	{
		private static readonly Random Rand = new(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		private Game CurrentGame { get; set; }

		private GameSettings GameSettings { get; } = new(
			new MinPlayerSetting(2),
			new MaxPlayerSetting(6),
			new NbCoinToWinSetting(6),
			new NbPlacesSetting(12));

		public GameRunner()
		{
			Domains.OnDomainTriggered += OnDomainTriggered;
		}

		public void Launch()
		{
			if (CurrentGame != null)
			{
				CurrentGame.Restart();
				return;
			}

			Game.StartNewGame(GameSettings,
				new PlayerInfo("Chet", 14),
				new PlayerInfo("Pat", 16),
				new PlayerInfo("Sue", 15));
		}

		private int _r2;

		private void OnDomainTriggered(IDomainBase domainEvent)
		{
			switch (domainEvent)
			{
				case GameStarted gameStarted:
					CurrentGame = gameStarted.Game;
					Console.WriteLine("Game started");
					Console.WriteLine();
					break;

				case PlayerAddedToGame playerAddedToGame:
					Console.WriteLine($"{playerAddedToGame.Player} was Added");
					Console.WriteLine($"They are player number {playerAddedToGame.Player.Number}");
					break;

				case PlayerRollRequested playerRollRequested:
					int r1 = Rand.Next(5) + 1;
					_r2 = Rand.Next(9);
					Console.WriteLine($"{playerRollRequested.Player} is the current player");
					Console.WriteLine($"They have rolled a {r1}");
					playerRollRequested.Response(new Roll(r1));
					break;

				case PlayerResponseRequested playerResponseRequested:
					Console.WriteLine($"{playerResponseRequested.Player}'s new location is {playerResponseRequested.Player.Place.Location}");
					Console.WriteLine($"The category is {playerResponseRequested.Player.Place.Category}");
					Console.WriteLine(playerResponseRequested.Question);
					playerResponseRequested.Response(new Response(_r2));
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
					Console.WriteLine($"{playerGoodResponseSended.Player} now has {playerGoodResponseSended.Player.Score} Gold Coins.");
					break;
			}
		}

		public void Dispose()
		{
			Domains.OnDomainTriggered -= OnDomainTriggered;
		}
	}
}
