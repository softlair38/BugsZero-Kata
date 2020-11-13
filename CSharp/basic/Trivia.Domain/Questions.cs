using System;
using System.Collections.Generic;

namespace Trivia
{
	internal class Questions<T> where T : struct
	{
		private Dictionary<T, Queue<string>> DicoQuestions { get; }

		private int _index;

		public int NbCategories { get; }

		public Questions()
		{
			NbCategories = Enum.GetValues(typeof(T)).Length;

			DicoQuestions = new Dictionary<T, Queue<string>>(NbCategories);

			foreach (T category in Enum.GetValues(typeof(T)))
				DicoQuestions.Add(category, new Queue<string>());
		}

		private void FillAll()
		{
			foreach (T category in Enum.GetValues(typeof(T)))
				Fill(category, _index, _index + 5);

			_index += 5;
		}

		private void Fill(T category, int start, int end)
		{
			var queue = DicoQuestions[category];
			for (int i = start; i < end; i++)
				queue.Enqueue($"{category} Question {i}");
		}

		public string GetNewOne(T category)
		{
			Queue<string> queue = DicoQuestions[category];
			if (queue.Count == 0)
				FillAll();

			return queue.Dequeue();
		}
	}
}
