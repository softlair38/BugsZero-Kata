using System.Collections.Generic;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Player
	{
		public string Name { get; }

		private RollingList<Place> Places { get; }

		public Place Place => Places.Current;

		public Purse Purse { get; }

		public bool InPenaltyBox { get; private set; }

		private Game Game { get; set; }

		internal Player(string name, IList<Place> places, Purse purse)
		{
			Name = name;
			Places = new RollingList<Place>(places);
			Purse = purse;
		}

		internal void ResetGame(Game game)
		{
			Game = game;
			Places.Reset();
			Purse.Reset();
			InPenaltyBox = false;
		}

		internal void Roll(Roll roll)
		{
			if (InPenaltyBox && roll.IsGettingOutOfPenaltyBox)
				SetNotInPenaltyBox();

			if (InPenaltyBox)
			{
				Domains.RaiseEvent(new PlayerStayedInPenaltyBox(Game, this));
				Game.NextPlayer();
			}
			else
			{
				Places.Next(roll.Number);
				Game.AskQuestion(Place.Category);
			}
		}

		internal void WasCorrectlyAnswered()
		{
			Purse.WinCoin(1);
			Domains.RaiseEvent(new PlayerGoodResponseSended(Game, this));

			if (Purse.HasWin)
				Domains.RaiseEvent(new GameEnded(Game));
			else
				Game.NextPlayer();
		}

		internal void WrongAnswer()
		{
			Domains.RaiseEvent(new PlayerBadResponseSended(Game, this));
			InPenaltyBox = true;

			Domains.RaiseEvent(new PlayerWentToPenaltyBox(Game, this));
			Game.NextPlayer();
		}

		private void SetNotInPenaltyBox()
		{
			InPenaltyBox = false;
			Domains.RaiseEvent(new PlayerGoOutOfPenaltyBox(Game, this));
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
