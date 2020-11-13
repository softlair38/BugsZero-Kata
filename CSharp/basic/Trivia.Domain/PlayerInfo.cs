namespace Trivia
{
	public class PlayerInfo
	{
		public string Name { get; }

		public int Age { get; }

		public PlayerInfo(string name, int age)
		{
			Name = name;
			Age = age;
		}
	}
}