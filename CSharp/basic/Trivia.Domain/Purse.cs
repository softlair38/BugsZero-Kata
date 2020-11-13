namespace Trivia
{
	public class Purse
	{
		public int Coin { get; private set; }

		public NbCoinToWinSetting NbPurseToWin { get; }

		public bool HasWin => Coin == NbPurseToWin.Value;

		internal Purse(NbCoinToWinSetting nbCoinToWin)
		{
			NbPurseToWin = nbCoinToWin;
		}

		internal void WinCoin(int nb)
		{
			Coin += nb;
		}

		internal void Reset()
		{
			Coin = 0;
		}
	}
}
