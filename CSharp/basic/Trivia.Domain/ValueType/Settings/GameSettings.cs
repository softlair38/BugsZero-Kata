namespace Trivia.Domain.ValueType.Settings
{
	public record GameSettings
	{
		public MinPlayerSetting MinPlayers { get; }
		public MaxPlayerSetting MaxPlayers { get; }
		public NbCoinToWinSetting NbCoinToWin { get; }
		public NbPlacesSetting NbPlaces { get; }

		public GameSettings(MinPlayerSetting minPlayers, MaxPlayerSetting maxPlayers, NbCoinToWinSetting nbCoinToWin, NbPlacesSetting nbPlaces)
		{
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
			NbCoinToWin = nbCoinToWin;
			NbPlaces = nbPlaces;
		}
	}
}
