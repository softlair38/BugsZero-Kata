using System.Collections.Generic;
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

			var list = new List<Player>();
			foreach (PlayerInfo playerInfo in playerInfos)
			{
				var player = new Player(playerInfo, places, new Score(nbCoinToWin), list.Count + 1, Game);
				list.Add(player);
				Domains.RaiseEvent(new PlayerAddedToGame(game, player));
			}

			Values = new RollingList<Player>(list);
		}

		internal void GoToNextPlayer()
		{
			Values.Next();
			Domains.RaiseRequest(new PlayerRollRequested(Game, Current));
		}

		internal void Reset()
		{
			Values.Reset();
			foreach (Player player in Values.InternalValues)
			{
				player.Reset();
			}
		}
	}
}
