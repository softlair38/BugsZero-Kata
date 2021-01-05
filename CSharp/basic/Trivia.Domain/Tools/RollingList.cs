using System.Collections.Generic;
using System.Linq;

namespace Trivia.Domain.Tools
{
	internal class RollingList<T>
	{
		public IList<T> InternalValues { get; }

		public T Current { get; private set; }

		private readonly object _lockCurrent = new();

		public RollingList(IList<T> values) : this(values, values.First())
		{ }

		public RollingList(IList<T> values, T start)
		{
			InternalValues = values.ToList();
			Current = start;
		}

		public void Next(int step = 1)
		{
			lock (_lockCurrent)
			{
				int i = InternalValues.IndexOf(Current) + step;

				if (i >= InternalValues.Count)
					i -= InternalValues.Count;

				Current = InternalValues[i];
			}
		}

		public void Reset()
		{
			lock (_lockCurrent)
				Current = InternalValues.First();
		}
	}
}
