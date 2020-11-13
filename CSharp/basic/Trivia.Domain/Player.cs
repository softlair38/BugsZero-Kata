using System.Collections.Generic;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Player
	{
		public PlayerInfo Info { get; }

		private RollingList<Place> Places { get; }

		public Place Place => Places.Current;

		public Purse Purse { get; }

		public bool InPenaltyBox { get; private set; }

		public int Number { get; private set; }

		private Game Game { get; set; }

		internal Player(PlayerInfo playerInfo, IList<Place> places, Purse purse)
		{
			Info = playerInfo;
			Places = new RollingList<Place>(places);
			Purse = purse;
		}

		internal void ResetGame(Game game, int number)
		{
			Game = game;
			Number = number;
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
			return Info.Name;
		}
	}
}
