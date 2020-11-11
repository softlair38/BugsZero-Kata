namespace Trivia
{
	public class Player
	{
		public string Name { get; }

		public int Places { get; private set; }
		public int Purses { get; private set; }

		public bool InPenaltyBox { get; private set; }

		public Player(string name)
		{
			Name = name;
		}

		public void ResetGame()
		{
			Places = 0;
			Purses = 0;
			InPenaltyBox = false;
		}

		public void Move(Roll roll)
		{
			Places += roll.Number;

			if (Places > 11)
				Places -= 12;
		}

		public void AddPurse()
		{
			Purses++;
		}

		public void SetInPenaltyBox()
		{
			InPenaltyBox = true;
		}

		public bool HasWin(int nbPurseToWin)
		{
			return Purses == nbPurseToWin;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
