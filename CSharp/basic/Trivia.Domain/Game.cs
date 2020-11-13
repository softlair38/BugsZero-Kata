using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private Questions<Category> Questions { get; } = new Questions<Category>();

		private RollingList<Player> Players { get; }

		private Places Places { get; }

		private Player CurrentPlayer => Players.Current;

		public static void StartNewGame(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			if (playerInfos == null || playerInfos.Length < settings.MinPlayers.Value || playerInfos.Length > settings.MaxPlayers.Value)
			{
				Domains.RaiseEvent(new GameErrorOccured($"Le nombre de joueur doit être compris entre {settings.MinPlayers} et {settings.MaxPlayers}"));
				return;
			}

			var game = new Game(settings, playerInfos);

			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.CurrentPlayer));
		}

		private Game(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			Places = new Places(settings.NbPlaces, Questions);

			List<Player> players = playerInfos
				.Select(p => new Player(p, Places, new Score(settings.NbCoinToWin)))
				.ToList();

			int number = 0;
			foreach (Player player in players)
			{
				number++;
				Add(player, number);
			}

			Players = new RollingList<Player>(players);
		}

		private void Add(Player player, int number)
		{
			player.ResetGame(this, number);
			Domains.RaiseEvent(new PlayerAddedToGame(this, player));
		}

		internal void AskQuestion(Category category)
		{
			Domains.RaiseRequest(new PlayerResponseRequested(this, CurrentPlayer, Questions.GetNewOne(category)));
		}

		internal void NextPlayer()
		{
			Players.Next();
			Domains.RaiseRequest(new PlayerRollRequested(this, CurrentPlayer));
		}
	}
}
