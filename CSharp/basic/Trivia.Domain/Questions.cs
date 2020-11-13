using System;
using System.Collections.Generic;

namespace Trivia
{
	internal class Questions
	{
		private Dictionary<Category, Queue<string>> DicoQuestions { get; } = new Dictionary<Category, Queue<string>>();

		private int _index;

		public Questions()
		{
			foreach (Category category in Enum.GetValues(typeof(Category)))
				DicoQuestions.Add(category, new Queue<string>());
		}

		private void FillAll()
		{
			foreach (Category category in Enum.GetValues(typeof(Category)))
				Fill(category, _index, _index + 5);

			_index += 5;
		}

		private void Fill(Category category, int start, int end)
		{
			var queue = DicoQuestions[category];
			for (int i = start; i < end; i++)
			{
				queue.Enqueue($"{category} Question {i}");
			}
		}

		public string GetNewOne(Category category)
		{
			Queue<string> queue = DicoQuestions[category];
			if (queue.Count == 0)
				FillAll();

			return queue.Dequeue();
		}
	}
}
