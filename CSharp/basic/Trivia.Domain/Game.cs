using System.Linq;
using Trivia.Domain.Events.Base;
using Trivia.Domain.Events.Games;
using Trivia.Domain.Events.Players;
using Trivia.Domain.ValueType;
using Trivia.Domain.ValueType.Settings;

namespace Trivia.Domain
{
	public class Game
	{
		internal Questions<Category> Questions { get; } = new();

		internal Players Players { get; }

		public static void StartNewGame(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			if (playerInfos == null || playerInfos.Length < settings.MinPlayers.Value || playerInfos.Length > settings.MaxPlayers.Value)
			{
				Domains.RaiseEvent(new GameErrorOccured($"Le nombre de joueur doit être compris entre {settings.MinPlayers} et {settings.MaxPlayers}"));
				return;
			}

			Game game = new(settings, playerInfos);
			game.Start();
		}

		private Game(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			var values = Enumerable.Range(0, settings.NbPlaces.Value)
				.Select(place => new Place((Category)(place % Questions.NbCategories), new Location(place)))
				.ToList();
			Places places = new(values);
			Players = new Players(playerInfos, settings.NbCoinToWin, places, this);
		}

		public void Restart()
		{
			Players.Reset();
			Questions.Reset();

			Start();
		}

		private void Start()
		{
			Domains.RaiseEvent(new GameStarted(this));
			Domains.RaiseRequest(new PlayerRollRequested(this, Players.Current));
		}
	}
}
