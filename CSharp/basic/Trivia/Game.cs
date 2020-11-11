using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
	public class Game
	{
		private List<Player> Players { get; } = new List<Player>();

		private const byte MaxPlayers = 6;

		private static Dictionary<Category, Queue<string>> DicoQuestions { get; } = new Dictionary<Category, Queue<string>>();

		private Player CurrentPlayer { get; set; }

		public Game()
		{
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

		public bool IsPlayable()
		{
			return HowManyPlayers() >= 2;
		}

		public void Add(Player player)
		{
			if (HowManyPlayers() == MaxPlayers)
				return;

			Players.Add(player);
			player.ResetGame();

			Console.WriteLine($"{player} was Added");
			Console.WriteLine($"They are player number {Players.Count}");

			CurrentPlayer = Players.First();
		}

		private int HowManyPlayers()
		{
			return Players.Count;
		}

		public void Roll(Roll roll)
		{
			if (!IsPlayable())
				return;

			Console.WriteLine($"{CurrentPlayer} is the current player");
			Console.WriteLine($"They have rolled a {roll}");

			if (!CurrentPlayer.InPenaltyBox)
			{
				CurrentPlayer.Move(roll);
				Console.WriteLine($"{CurrentPlayer}'s new location is {CurrentPlayer.Places}");
				Console.WriteLine($"The category is {CurrentCategory()}");
				AskQuestion();
				return;
			}

			if (roll.IsGettingOutOfPenaltyBox)
			{
				Console.WriteLine($"{CurrentPlayer} is getting out of the penalty box");

				CurrentPlayer.Move(roll);
				Console.WriteLine($"{CurrentPlayer}'s new location is {CurrentPlayer.Places}");
				Console.WriteLine($"The category is {CurrentCategory()}");
				AskQuestion();
			}
			else
			{
				Console.WriteLine($"{CurrentPlayer} is not getting out of the penalty box");
			}
		}

		private void AskQuestion()
		{
			switch (CurrentCategory())
			{
				case Category.Pop:
				case Category.Rock:
				case Category.Science:
				case Category.Sports:
					Console.WriteLine(DicoQuestions[CurrentCategory()].Dequeue());
					break;
			}
		}

		private Category CurrentCategory()
		{
			switch (CurrentPlayer.Places)
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

		public bool WasCorrectlyAnswered(Roll roll)
		{
			if (!CurrentPlayer.InPenaltyBox)
			{
				Console.WriteLine("Answer was corrent!!!!");
				CurrentPlayer.AddPurse();
				Console.WriteLine($"{CurrentPlayer} now has {CurrentPlayer.Purses} Gold Coins.");

				bool winner = DidPlayerWin();

				NextPlayer();

				return winner;
			}

			if (roll.IsGettingOutOfPenaltyBox)
			{
				Console.WriteLine("Answer was correct!!!!");

				NextPlayer();

				CurrentPlayer.AddPurse();
				Console.WriteLine($"{CurrentPlayer} now has {CurrentPlayer.Purses} Gold Coins.");

				return DidPlayerWin();
			}

			NextPlayer();
			return true;
		}

		private void NextPlayer()
		{
			CurrentPlayer = Players.IndexOf(CurrentPlayer) == Players.Count - 1
				? Players.First()
				: Players[Players.IndexOf(CurrentPlayer) + 1];
		}

		public bool WrongAnswer()
		{
			Console.WriteLine("Question was incorrectly answered");
			Console.WriteLine($"{CurrentPlayer} was sent to the penalty box");
			CurrentPlayer.SetInPenaltyBox();

			NextPlayer();
			return true;
		}

		private bool DidPlayerWin()
		{
			return CurrentPlayer.Purses != 6;
		}
	}
}
