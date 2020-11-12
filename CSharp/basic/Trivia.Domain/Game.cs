using System;
using System.Collections.Generic;
using System.Linq;
using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private List<Player> Players { get; }

		private const ushort MinPlayers = 2;
		private const ushort MaxPlayers = 6;
		internal static ushort NbPurseToWin = 6;
		internal static ushort NbPlaces = 12;

		private static Dictionary<Category, Queue<string>> DicoQuestions { get; } = new Dictionary<Category, Queue<string>>();

		private Player CurrentPlayer { get; set; }

		public static void StartNewGame(params Player[] players)
		{
			var game = new Game(players);
			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.CurrentPlayer));
		}

		private Game(params Player[] players)
		{
			if (players == null || players.Length < MinPlayers || players.Length > MaxPlayers)
				throw new InvalidOperationException($"Le nombre de joueur doit être compris entre {MinPlayers} et {MaxPlayers}");

			Players = new List<Player>();
			foreach (Player player in players)
			{
				Add(player);
			}
			CurrentPlayer = Players.First();

			DicoQuestions.Clear();
			DicoQuestions.Add(Category.Pop, new Queue<string>());
			DicoQuestions.Add(Category.Science, new Queue<string>());
			DicoQuestions.Add(Category.Sports, new Queue<string>());
			DicoQuestions.Add(Category.Rock, new Queue<string>());

			for (int i = 0; i < 50; i++)
			{
				DicoQuestions[Category.Pop].Enqueue($"Pop Question {i}");
				DicoQuestions[Category.Science].Enqueue($"Science Question {i}");
				DicoQuestions[Category.Sports].Enqueue($"Sports Question {i}");
				DicoQuestions[Category.Rock].Enqueue($"Rock Question {i}");
			}
		}

		private void Add(Player player)
		{
			Players.Add(player);
			player.ResetGame();
			Domains.RaiseEvent(new PlayerAddedToGame(this, player, Players.Count));
		}

		internal void Roll(Roll roll)
		{
			if (CurrentPlayer.InPenaltyBox)
				if (roll.IsGettingOutOfPenaltyBox)
				{
					CurrentPlayer.SetNotInPenaltyBox();
					Domains.RaiseEvent(new PlayerGoOutOfPenaltyBox(this, CurrentPlayer));
				}
				else
				{
					Domains.RaiseEvent(new PlayerStayedInPenaltyBox(this, CurrentPlayer));
					NextPlayer();
					return;
				}

			CurrentPlayer.Move(roll);
			Category category = CurrentCategory(CurrentPlayer.Places);
			string question = DicoQuestions[category].Dequeue();
			Domains.RaiseRequest(new PlayerResponseRequested(this, CurrentPlayer, question, category.ToString()));
		}

		private Category CurrentCategory(int place)
		{
			switch (place)
			{
				case 0:
				case 4:
				case 8:
					return Category.Pop;

				case 1:
				case 5:
				case 9:
					return Category.Science;

				case 2:
				case 6:
				case 10:
					return Category.Sports;

				default:
					return Category.Rock;
			}
		}

		internal void WasCorrectlyAnswered()
		{
			CurrentPlayer.AddPurse();
			Domains.RaiseEvent(new PlayerGoodResponseSended(this, CurrentPlayer));

			if (CurrentPlayer.HasWin())
				Domains.RaiseEvent(new GameEnded(this));
			else
				NextPlayer();
		}

		private void NextPlayer()
		{
			CurrentPlayer = Players.IndexOf(CurrentPlayer) == Players.Count - 1
				? Players.First()
				: Players[Players.IndexOf(CurrentPlayer) + 1];

			Domains.RaiseRequest(new PlayerRollRequested(this, CurrentPlayer));
		}

		internal void WrongAnswer()
		{
			Domains.RaiseEvent(new PlayerBadResponseSended(this, CurrentPlayer));

			CurrentPlayer.SetInPenaltyBox();
			Domains.RaiseEvent(new PlayerWentToPenaltyBox(this, CurrentPlayer));

			NextPlayer();
		}
	}
}
