using Trivia.Domain.Events.Base;
using Trivia.Domain.Events.Games;
using Trivia.Domain.Events.Players;
using Trivia.Domain.Tools;
using Trivia.Domain.ValueType;

namespace Trivia.Domain
{
	public class Player
	{
		public PlayerInfo Info { get; }

		private RollingList<Place> Places { get; }

		public Place Place => Places.Current;

		public Score Score { get; }

		public bool InPenaltyBox { get; private set; }

		public int Number { get; }

		private Game Game { get; }

		internal Player(PlayerInfo playerInfo, Places places, Score score, int number, Game game)
		{
			Info = playerInfo;
			Places = new RollingList<Place>(places.Values);
			Score = score;
			Number = number;
			Game = game;

			Domains.RaiseEvent(new PlayerAddedToGame(game, this));
		}

		internal void Reset()
		{
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
				AskQuestion(Place.Category);
			}
		}

		private void AskQuestion(Category category)
		{
			Domains.RaiseRequest(new PlayerResponseRequested(Game, this, Game.Questions.GetNewOne(category)));
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
