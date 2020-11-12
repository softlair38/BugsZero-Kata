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

		internal void ResetGame()
		{
			Places = 0;
			Purses = 0;
			InPenaltyBox = false;
		}

		internal void Move(Roll roll)
		{
			Places += roll.Number;

			if (Places >= Game.NbPlaces)
				Places -= Game.NbPlaces;
		}

		internal void AddPurse()
		{
			Purses++;
		}

		internal void SetInPenaltyBox()
		{
			InPenaltyBox = true;
		}

		internal void SetNotInPenaltyBox()
		{
			InPenaltyBox = false;
		}

		internal bool HasWin()
		{
			return Purses == Game.NbPurseToWin;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
