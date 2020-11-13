using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
	internal readonly struct Places
	{
		internal IList<Place> Values { get; }

		internal Places(NbPlacesSetting nbPlaces, Questions<Category> questions)
		{
			Values = Enumerable.Range(0, nbPlaces.Value)
				.Select(place => new Place((Category)(place % questions.NbCategories), new Location(place)))
				.ToList();
		}
	}
}
