using System.Collections.Generic;
using Trivia.Domain.Events.Base;
using Trivia.Domain.Events.Players;
using Trivia.Domain.Tools;
using Trivia.Domain.ValueType;
using Trivia.Domain.ValueType.Settings;

namespace Trivia.Domain
{
	internal class Players
	{
		private Game Game { get; }

		private RollingList<Player> RollingPlayers { get; }

		internal Player Current => RollingPlayers.Current;

		internal Players(IList<PlayerInfo> playerInfos, NbCoinToWinSetting nbCoinToWin, Places places, Game game)
		{
			Game = game;

			List<Player> list = new();
			foreach (PlayerInfo playerInfo in playerInfos)
				list.Add(new(playerInfo, places, new Score(nbCoinToWin), list.Count + 1, Game));

			RollingPlayers = new RollingList<Player>(list);
		}

		internal void GoToNextPlayer()
		{
			RollingPlayers.Next();
			Domains.RaiseRequest(new PlayerRollRequested(Current));
		}

		internal void Reset()
		{
			RollingPlayers.Reset();

			foreach (Player player in RollingPlayers.InternalValues)
				player.Reset();
		}
	}
}
