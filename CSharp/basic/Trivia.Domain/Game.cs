using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private Questions<Category> Questions { get; } = new Questions<Category>();

		private RollingList<Player> Players { get; }

		private IList<Place> Places { get; }

		private Player CurrentPlayer => Players.Current;

		public static void StartNewGame(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			if (playerInfos == null || playerInfos.Length < settings.MinPlayers || playerInfos.Length > settings.MaxPlayers)
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
			Places = Enumerable.Range(0, settings.NbPlaces)
				.Select(place => new Place((Category)(place % Questions.NbCategories), place))
				.ToList();

			List<Player> players = playerInfos
				.Select(p => new Player(p, Places, new Purse(settings.NbPurseToWin)))
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
			player.ResetGame(this);
			Domains.RaiseEvent(new PlayerAddedToGame(this, player, number));
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
