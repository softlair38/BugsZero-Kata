using Trivia.Domain.ValueType.Settings;

namespace Trivia.Domain
{
	public class Score
	{
		private int Coin { get; set; }

		private NbCoinToWinSetting NbCoinToWin { get; }

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

		public override string ToString()
		{
			return Coin.ToString();
		}
	}
}
