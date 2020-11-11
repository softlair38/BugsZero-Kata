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

		public Game(params Player[] players)
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

			Console.WriteLine($"{player} was Added");
			Console.WriteLine($"They are player number {Players.Count}");
		}

		public void Roll(Roll roll)
		{
			Console.WriteLine($"{CurrentPlayer} is the current player");
			Console.WriteLine($"They have rolled a {roll}");

			if (CurrentPlayer.InPenaltyBox)
				if (roll.IsGettingOutOfPenaltyBox)
				{
					Console.WriteLine($"{CurrentPlayer} is getting out of the penalty box");
				}
				else
				{
					Console.WriteLine($"{CurrentPlayer} is not getting out of the penalty box");
					NextPlayer();
					return;
				}

			CurrentPlayer.Move(roll);
			Console.WriteLine($"{CurrentPlayer}'s new location is {CurrentPlayer.Places}");
			Console.WriteLine($"The category is {CurrentCategory()}");

			AskQuestion();
			if (roll.GoodAnswer)
				WrongAnswer();
			else
				WasCorrectlyAnswered();
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
					DomainEvent.Raise(new PlayerResponseRequested(this));
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

		private void WasCorrectlyAnswered()
		{
			Console.WriteLine("Answer was correct!!!!");
			CurrentPlayer.AddPurse();
			Console.WriteLine($"{CurrentPlayer} now has {CurrentPlayer.Purses} Gold Coins.");

			if (!CurrentPlayer.HasWin())
				NextPlayer();
		}

		private void NextPlayer()
		{
			CurrentPlayer = Players.IndexOf(CurrentPlayer) == Players.Count - 1
				? Players.First()
				: Players[Players.IndexOf(CurrentPlayer) + 1];

			DomainEvent.Raise(new PlayerRollRequested(this));
		}

		private void WrongAnswer()
		{
			Console.WriteLine("Question was incorrectly answered");
			Console.WriteLine($"{CurrentPlayer} was sent to the penalty box");
			CurrentPlayer.SetInPenaltyBox();

			NextPlayer();
		}
	}
}
