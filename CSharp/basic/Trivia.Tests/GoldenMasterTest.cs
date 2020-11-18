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

			{
				using FileStream fileStream = new(outputPath, FileMode.CreateNew, FileAccess.Write);
				using StreamWriter streamWriter = new(fileStream);
				
				TextWriter oldOut = Console.Out;
				Console.SetOut(streamWriter);

				using GameRunner gameRunner = new();
				for (int i = 0; i < 20; i++)
				{
					Console.WriteLine();
					gameRunner.Launch();
				}

				Console.SetOut(oldOut);
			}

			Assert.AreEqual(File.ReadAllText(outputPath), File.ReadAllText("Expected.txt"));
		}
	}
}
