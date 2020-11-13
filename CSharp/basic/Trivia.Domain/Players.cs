using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	internal class Players
	{
		private Game Game { get; }

		private RollingList<Player> Values { get; }

		internal Player Current => Values.Current;

		internal Players(IList<PlayerInfo> playerInfos, NbCoinToWinSetting nbCoinToWin, Places places, Game game)
		{
			Game = game;
			List<Player> players = playerInfos
				.Select(p => new Player(p, places, new Score(nbCoinToWin)))
				.ToList();

			int number = 0;
			foreach (Player player in players)
			{
				number++;
				player.ResetGame(game, number);
				Domains.RaiseEvent(new PlayerAddedToGame(game, player));
			}

			Values = new RollingList<Player>(players);
		}

		internal void GoToNextPlayer()
		{
			Values.Next();
			Domains.RaiseRequest(new PlayerRollRequested(Game, Current));
		}
	}
}
