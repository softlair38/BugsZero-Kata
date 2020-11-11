using System;

namespace Trivia
{
	public static class GameRunner
	{
		private static readonly Random Rand = new Random(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		private static bool _notAWinner;

		public static void Main(string[] args)
		{
			Game aGame = new Game();

			aGame.Add(new Player("Chet"));
			aGame.Add(new Player("Pat"));
			aGame.Add(new Player("Sue"));

			Random rand = Rand;

			do
			{
				aGame.Roll(rand.Next(5) + 1);

				_notAWinner = rand.Next(9) == 7
					? aGame.WrongAnswer()
					: aGame.WasCorrectlyAnswered();

			} while (_notAWinner);
		}
	}
}
