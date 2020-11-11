namespace Trivia
{
	public class Player
	{
		internal string Name { get; }

		internal int Places { get; private set; }
		internal int Purses { get; private set; }

		internal bool InPenaltyBox { get; private set; }

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
