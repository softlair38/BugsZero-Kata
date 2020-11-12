using System.Collections.Generic;

namespace Trivia
{
	internal class Questions
	{
		private Dictionary<Category, Queue<string>> DicoQuestions { get; } = new Dictionary<Category, Queue<string>>();

		public Questions()
		{
			Fill(Category.Pop);
			Fill(Category.Science);
			Fill(Category.Sports);
			Fill(Category.Rock);
		}

		private void Fill(Category category)
		{
			DicoQuestions.Add(category, new Queue<string>());
			for (int i = 0; i < 50; i++)
			{
				DicoQuestions[category].Enqueue($"{category} Question {i}");
			}
		}

		public string GetNewOne(Category category)
		{
			return DicoQuestions[category].Dequeue();
		}
	}
}
