namespace Trivia
{
	public readonly struct GameSettings
	{
		public MinPlayerSetting MinPlayers { get; }
		public MaxPlayerSetting MaxPlayers { get; }
		public NbCoinToWinSetting NbPurseToWin { get; }
		public NbPlacesSetting NbPlaces { get; }

		public GameSettings(MinPlayerSetting minPlayers, MaxPlayerSetting maxPlayers, NbCoinToWinSetting nbPurseToWin, NbPlacesSetting nbPlaces)
		{
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
			NbPurseToWin = nbPurseToWin;
			NbPlaces = nbPlaces;
		}
	}
}
