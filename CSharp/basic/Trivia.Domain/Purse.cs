namespace Trivia
{
	public class Purse
	{
		public int Coin { get; private set; }

		public int NbPurseToWin { get; }

		public bool HasWin => Coin == NbPurseToWin;

		internal Purse(int nbPurseToWin)
		{
			NbPurseToWin = nbPurseToWin;
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
