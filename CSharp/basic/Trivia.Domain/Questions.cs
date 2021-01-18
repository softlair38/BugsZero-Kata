using System;
using System.Collections.Generic;
using Trivia.Domain.ValueType;

namespace Trivia.Domain
{
	internal class Questions<T> where T : struct
	{
		private Dictionary<T, Queue<Question>> DicoQuestions { get; }

		private int Index { get; set; }

		internal int NbCategories { get; }

		internal Questions()
		{
			NbCategories = Enum.GetValues(typeof(T)).Length;

			DicoQuestions = new Dictionary<T, Queue<Question>>(NbCategories);

			foreach (T category in Enum.GetValues(typeof(T)))
				DicoQuestions.Add(category, new Queue<Question>());
		}

		private void FillAll()
		{
			foreach (T category in Enum.GetValues(typeof(T)))
				Fill(category, Index, Index + 5);

			Index += 5;
		}

		private void Fill(T category, int start, int end)
		{
			Queue<Question> queue = DicoQuestions[category];
			for (int i = start; i < end; i++)
				queue.Enqueue(new Question($"{category} Question {i}", new Response(7)));
		}

		internal Question GetNewOne(T category)
		{
			Queue<Question> queue = DicoQuestions[category];
			if (queue.Count == 0)
				FillAll();

			return queue.Dequeue();
		}

		internal void Reset()
		{
			foreach (Queue<Question> questions in DicoQuestions.Values)
				questions.Clear();

			Index = 0;
		}
	}
}
