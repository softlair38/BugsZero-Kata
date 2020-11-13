using Trivia.Domain.Events;

namespace Trivia
{
	public class Player
	{
		public PlayerInfo Info { get; }

		private RollingList<Place> Places { get; }

		public Place Place => Places.Current;

		public Score Score { get; }

		public bool InPenaltyBox { get; private set; }

		public int Number { get; private set; }

		private Game Game { get; set; }

		internal Player(PlayerInfo playerInfo, Places places, Score score)
		{
			Info = playerInfo;
			Places = new RollingList<Place>(places.Values);
			Score = score;
		}

		internal void ResetGame(Game game, int number)
		{
			Game = game;
			Number = number;
			Places.Reset();
			Score.Reset();
			InPenaltyBox = false;
		}

		internal void Roll(Roll roll)
		{
			if (InPenaltyBox && roll.IsGettingOutOfPenaltyBox)
				SetNotInPenaltyBox();

			if (InPenaltyBox)
			{
				Domains.RaiseEvent(new PlayerStayedInPenaltyBox(Game, this));
				Game.Players.GoToNextPlayer();
			}
			else
			{
				Places.Next(roll.Number);
				Game.AskQuestion(Place.Category);
			}
		}

		internal void WasCorrectlyAnswered()
		{
			Score.WinCoin(1);
			Domains.RaiseEvent(new PlayerGoodResponseSended(Game, this));

			if (Score.HasWin)
				Domains.RaiseEvent(new GameEnded(Game));
			else
				Game.Players.GoToNextPlayer();
		}

		internal void WrongAnswer()
		{
			Domains.RaiseEvent(new PlayerBadResponseSended(Game, this));
			InPenaltyBox = true;

			Domains.RaiseEvent(new PlayerWentToPenaltyBox(Game, this));
			Game.Players.GoToNextPlayer();
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
