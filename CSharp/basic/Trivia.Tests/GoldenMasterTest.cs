using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Trivia.Tests
{
	[TestClass]
	public class GoldenMasterTest
	{
		[TestMethod]
		public void GoldenMaster()
		{
			const string outputPath = "Output.txt";

			if (File.Exists(outputPath))
				File.Delete(outputPath);

			using (var fileStream = new FileStream(outputPath, FileMode.CreateNew, FileAccess.Write))
			{
				using (var streamWriter = new StreamWriter(fileStream))
				{
					TextWriter oldOut = Console.Out;
					Console.SetOut(streamWriter);

					for (int i = 0; i < 20; i++)
					{
						Console.WriteLine();
						GameRunner.Main(null);
					}

					Console.SetOut(oldOut);
				}
			}

			Assert.AreEqual(File.ReadAllText(outputPath), File.ReadAllText("Expected.txt"));
		}
	}
}
