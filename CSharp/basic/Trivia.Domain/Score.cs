namespace Trivia
{
	public class Score
	{
		public int Coin { get; private set; }

		public NbCoinToWinSetting NbCoinToWin { get; }

		public bool HasWin => Coin == NbCoinToWin.Value;

		internal Score(NbCoinToWinSetting nbCoinToWin)
		{
			NbCoinToWin = nbCoinToWin;
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
