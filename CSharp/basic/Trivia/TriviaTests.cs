using Assent;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Trivia
{
	public class TriviaTests
	{
		[Fact]
		public void RefactoringTests()
		{
			var output = new StringBuilder();
			Console.SetOut(new StringWriter(output));

			Console.WriteLine(false); // test à revoir
			Game aGame = new Game(new Player("Chet"), new Player("Pat"), new Player("Sue"));

			var roll1 = new Roll(1, 2);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);
			aGame.Roll(roll1);

			aGame.WasCorrectlyAnswered();
			aGame.WrongAnswer();

			var roll2 = new Roll(2, 3);
			aGame.Roll(roll2);

			aGame.Roll(new Roll(6, 7));

			aGame.WrongAnswer();

			aGame.Roll(roll2);

			aGame.Roll(roll2);


			aGame.WrongAnswer();

			//aGame.WasCorrectlyAnswered();
			aGame.Roll(roll1);
			aGame.WasCorrectlyAnswered();

			var configuration = BuildConfiguration();
			File.WriteAllText("expected.txt", output.ToString());
			this.Assent(output.ToString(), configuration);
		}

		private static Configuration BuildConfiguration()
		{
			return
				new Configuration()

				// Uncomment this block if an exception 
				// « Could not find a diff program to use »
				// is thrown and if you have VsCode installed.
				// Otherwise, use other DiffProgram with its full path
				// as parameter.
				// See  https://github.com/droyad/Assent/wiki/Reporting
				//                    .UsingReporter(
				//                        new DiffReporter(
				//                            new []
				//                            {
				//                                 For linux
				//                                new VsCodeDiffProgram(new []
				//                                {
				//                                    "/usr/bin/code"
				//                                })

				//                                 For Windows
				//                                new VsCodeDiffProgram(), 
				//                            }))
				;
		}
	}
}
