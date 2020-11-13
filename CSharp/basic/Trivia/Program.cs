namespace Trivia
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var gameRunner = new GameRunner())
			{
				gameRunner.Launch();
			}
		}
	}
}
