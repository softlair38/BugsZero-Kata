using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private Questions<Category> Questions { get; }

		private List<Player> Players { get; }
		private Player CurrentPlayer { get; set; }

		private const ushort MinPlayers = 2;
		private const ushort MaxPlayers = 6;
		internal const ushort NbPurseToWin = 6;
		internal const ushort NbPlaces = 12;

		public static void StartNewGame(params Player[] players)
		{
			if (players == null || players.Length < MinPlayers || players.Length > MaxPlayers)
			{
				Domains.RaiseEvent(new GameErrorOccured($"Le nombre de joueur doit être compris entre {MinPlayers} et {MaxPlayers}"));
				return;
			}

			var game = new Game(players);

			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.CurrentPlayer));
		}

		private Game(params Player[] players)
		{
			Questions = new Questions<Category>();

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
