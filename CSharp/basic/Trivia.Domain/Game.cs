using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		internal Questions<Category> Questions { get; } = new Questions<Category>();

		internal Players Players { get; }

		public static void StartNewGame(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			if (playerInfos == null || playerInfos.Length < settings.MinPlayers.Value || playerInfos.Length > settings.MaxPlayers.Value)
			{
				Domains.RaiseEvent(new GameErrorOccured($"Le nombre de joueur doit être compris entre {settings.MinPlayers} et {settings.MaxPlayers}"));
				return;
			}

			var game = new Game(settings, playerInfos);

			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.Players.Current));
		}

		private Game(GameSettings settings, params PlayerInfo[] playerInfos)
		{
			var places = new Places(settings.NbPlaces, Questions);
			Players = new Players(playerInfos, settings.NbCoinToWin, places, this);
		}
	}
}
