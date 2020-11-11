using System;
using System.Collections.Generic;

namespace Trivia
{
	public class Game
	{
		private List<string> Players { get; } = new List<string>();

		private int[] Places { get; } = new int[6];
		private int[] Purses { get; } = new int[6];

		private bool[] InPenaltyBox { get; } = new bool[6];

		private static Dictionary<Category, Queue<string>> DicoQuestions { get; } = new Dictionary<Category, Queue<string>>();

		private int CurrentPlayer { get; set; }

		private bool IsGettingOutOfPenaltyBox { get; set; }

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

		public void Add(string playerName)
		{
			Players.Add(playerName);
			Places[HowManyPlayers()] = 0;
			Purses[HowManyPlayers()] = 0;
			InPenaltyBox[HowManyPlayers()] = false;

			Console.WriteLine($"{playerName} was Added");
			Console.WriteLine($"They are player number {Players.Count}");
		}

		private int HowManyPlayers()
		{
			return Players.Count;
		}

		public void Roll(int roll)
		{
			Console.WriteLine($"{Players[CurrentPlayer]} is the current player");
			Console.WriteLine($"They have rolled a {roll}");

			if (!InPenaltyBox[CurrentPlayer])
			{
				Places[CurrentPlayer] = Places[CurrentPlayer] + roll;

				if (Places[CurrentPlayer] > 11)
					Places[CurrentPlayer] = Places[CurrentPlayer] - 12;

				Console.WriteLine($"{Players[CurrentPlayer]}'s new location is {Places[CurrentPlayer]}");
				Console.WriteLine($"The category is {CurrentCategory()}");
				AskQuestion();
				return;
			}

			if (roll % 2 != 0)
			{
				IsGettingOutOfPenaltyBox = true;

				Console.WriteLine($"{Players[CurrentPlayer]} is getting out of the penalty box");
				Places[CurrentPlayer] = Places[CurrentPlayer] + roll;

				if (Places[CurrentPlayer] > 11)
					Places[CurrentPlayer] = Places[CurrentPlayer] - 12;

				Console.WriteLine($"{Players[CurrentPlayer]}'s new location is {Places[CurrentPlayer]}");
				Console.WriteLine($"The category is {CurrentCategory()}");
				AskQuestion();
			}
			else
			{
				Console.WriteLine($"{Players[CurrentPlayer]} is not getting out of the penalty box");
				IsGettingOutOfPenaltyBox = false;
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
			switch (Places[CurrentPlayer])
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

		public bool WasCorrectlyAnswered()
		{
			if (!InPenaltyBox[CurrentPlayer])
			{
				Console.WriteLine("Answer was corrent!!!!");
				Purses[CurrentPlayer]++;
				Console.WriteLine($"{Players[CurrentPlayer]} now has {Purses[CurrentPlayer]} Gold Coins.");

				bool winner = DidPlayerWin();
				CurrentPlayer++;

				if (CurrentPlayer == Players.Count)
					CurrentPlayer = 0;

				return winner;
			}

			if (IsGettingOutOfPenaltyBox)
			{
				Console.WriteLine("Answer was correct!!!!");
				CurrentPlayer++;

				if (CurrentPlayer == Players.Count)
					CurrentPlayer = 0;

				Purses[CurrentPlayer]++;
				Console.WriteLine($"{Players[CurrentPlayer]} now has {Purses[CurrentPlayer]} Gold Coins.");

				return DidPlayerWin();
			}

			CurrentPlayer++;

			if (CurrentPlayer == Players.Count)
				CurrentPlayer = 0;

			return true;
		}

		public bool WrongAnswer()
		{
			Console.WriteLine("Question was incorrectly answered");
			Console.WriteLine($"{Players[CurrentPlayer]} was sent to the penalty box");
			InPenaltyBox[CurrentPlayer] = true;

			CurrentPlayer++;

			if (CurrentPlayer == Players.Count)
				CurrentPlayer = 0;

			return true;
		}

		private bool DidPlayerWin()
		{
			return Purses[CurrentPlayer] != 6;
		}
	}
}
