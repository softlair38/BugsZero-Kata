using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
	public class Game
	{
		private List<string> Players { get; } = new List<string>();

		private int[] Places { get; } = new int[6];
		private int[] Purses { get; } = new int[6];

		private bool[] InPenaltyBox { get; } = new bool[6];

		private LinkedList<string> PopQuestions { get; } = new LinkedList<string>();
		private LinkedList<string> ScienceQuestions { get; } = new LinkedList<string>();
		private LinkedList<string> SportsQuestions { get; } = new LinkedList<string>();
		private LinkedList<string> RockQuestions { get; } = new LinkedList<string>();

		private int CurrentPlayer { get; set; }

		private bool IsGettingOutOfPenaltyBox { get; set; }

		public Game()
		{
			for (int i = 0; i < 50; i++)
			{
				PopQuestions.AddLast($"Pop Question {i}");
				ScienceQuestions.AddLast($"Science Question {i}");
				SportsQuestions.AddLast($"Sports Question {i}");
				RockQuestions.AddLast(CreateRockQuestion(i));
			}
		}

		private string CreateRockQuestion(int index)
		{
			return $"Rock Question {index}";
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
				case "Pop":
					Console.WriteLine(PopQuestions.First());
					PopQuestions.RemoveFirst();
					break;

				case "Science":
					Console.WriteLine(ScienceQuestions.First());
					ScienceQuestions.RemoveFirst();
					break;

				case "Sports":
					Console.WriteLine(SportsQuestions.First());
					SportsQuestions.RemoveFirst();
					break;

				case "Rock":
					Console.WriteLine(RockQuestions.First());
					RockQuestions.RemoveFirst();
					break;
			}
		}

		private string CurrentCategory()
		{
			switch (Places[CurrentPlayer])
			{
				case 0:
				case 4:
				case 8:
					return "Pop";

				case 1:
				case 5:
				case 9:
					return "Science";

				case 2:
				case 6:
				case 10:
					return "Sports";

				default:
					return "Rock";
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
