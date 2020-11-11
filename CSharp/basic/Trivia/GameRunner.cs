using System;

namespace Trivia
{
	public class GameRunner
	{
		private static readonly Random Rand = new Random(new Guid("1BEFC143-CBA2-4F3D-9219-F2220F792D28").GetHashCode());

		private static bool notAWinner;

		public static void Main(String[] args)
		{
			Game aGame = new Game();

			aGame.Add("Chet");
			aGame.Add("Pat");
			aGame.Add("Sue");

			Random rand = Rand;

			do
			{

				aGame.Roll(rand.Next(5) + 1);

				if (rand.Next(9) == 7)
				{
					notAWinner = aGame.WrongAnswer();
				}
				else
				{
					notAWinner = aGame.WasCorrectlyAnswered();
				}



			} while (notAWinner);

		}


	}

}
