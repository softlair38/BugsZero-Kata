using System;
using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private List<Player> Players { get; }

		private const ushort MinPlayers = 2;
		private const ushort MaxPlayers = 6;
		internal static ushort NbPurseToWin = 6;
		internal static ushort NbPlaces = 12;

		private Questions Questions { get; } = new Questions();

		private Player CurrentPlayer { get; set; }

		public static void StartNewGame(params Player[] players)
		{
			var game = new Game(players);
			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.CurrentPlayer));
		}

		private Game(params Player[] players)
		{
			if (players == null || players.Length < MinPlayers || players.Length > MaxPlayers)
				throw new InvalidOperationException($"Le nombre de joueur doit être compris entre {MinPlayers} et {MaxPlayers}");

			Players = new List<Player>();
			foreach (Player player in players)
			{
				Add(player);
			}
			CurrentPlayer = Players.First();
		}

		private void Add(Player player)
		{
			player.ResetGame(this);
			Players.Add(player);
			Domains.RaiseEvent(new PlayerAddedToGame(this, player, Players.Count));
		}

		internal void AskQuestion()
		{
			Category category = CurrentCategory(CurrentPlayer.Places);
			Domains.RaiseRequest(new PlayerResponseRequested(this, CurrentPlayer, Questions.GetNewOne(category), category.ToString()));
		}

		private Category CurrentCategory(int place)
		{
			return (Category)(place % 4);
		}

		internal void NextPlayer()
		{
			CurrentPlayer = Players.IndexOf(CurrentPlayer) == Players.Count - 1
				? Players.First()
				: Players[Players.IndexOf(CurrentPlayer) + 1];

			Domains.RaiseRequest(new PlayerRollRequested(this, CurrentPlayer));
		}
	}
}
