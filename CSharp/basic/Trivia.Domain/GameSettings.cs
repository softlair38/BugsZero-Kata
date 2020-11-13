using System;

namespace Trivia
{
	public readonly struct GameSettings
	{
		public ushort MinPlayers { get; }
		public ushort MaxPlayers { get; }
		public ushort NbPurseToWin { get; }
		public ushort NbPlaces { get; }

		public GameSettings(ushort minPlayers, ushort maxPlayers, ushort nbPurseToWin, ushort nbPlaces)
		{
			if (minPlayers < 2) throw new ArgumentOutOfRangeException("min 2 players");
			if (maxPlayers > 50) throw new ArgumentOutOfRangeException("max 50 players");
			if (nbPurseToWin < 2) throw new ArgumentOutOfRangeException("min 2 purse to win");
			if (nbPlaces < 2) throw new ArgumentOutOfRangeException("min 2 places");

			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
			NbPurseToWin = nbPurseToWin;
			NbPlaces = nbPlaces;
		}
	}
}
