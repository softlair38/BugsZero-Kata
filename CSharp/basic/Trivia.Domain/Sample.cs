using System.ComponentModel;
using Trivia.Domain.Events.Base;

namespace Trivia.Domain
{
	#region 1 POCO
	public class Player1
	{
		public string Name { get; set; }

		public int Nb { get; set; }
	}
	#endregion

	#region 2 type primitif : string int bool ...
	public class Player2
	{
		public Name Name { get; set; }

		public Coin Score { get; set; }
	}

	public record Name(string FirstName);

	public record Coin(int Value);
	#endregion

	#region 3 private setters / ctor
	public class Player3
	{
		public static Player3 BuildNewOne(Name name, Coin coin)
		{
			if (coin.Value <= -2 || coin.Value >= 8)
				throw new InvalidEnumArgumentException("");

			return new(name, coin);
		}

		private Player3(Name name, Coin coin)
		{
			Name = name;
			Coin = coin;
		}

		public Name Name { get; }

		public Coin Coin { get; private set; }

		public void AddCoin(Coin coin)
		{
			if (coin.Value == 0)
				throw new InvalidEnumArgumentException("");

			Coin = new Coin(Coin.Value + coin.Value);
		}

		public bool HasWin()
		{
			return Coin.Value == 6;
		}
	}
	#endregion

	#region 4 public => private / internal => ou donner du sens métier
	public class Player4
	{
		public static void BuildNewOne(Name name, Coin coin)
		{
			if (coin.Value <= -2 || coin.Value >= 8)
			{
				Domains.RaiseEvent(new PlayerAddErrorOccured());
				return;
			}

			Domains.RaiseEvent(new PlayerAdded { Player4 = new(name, coin) });
		}

		private Player4(Name name, Coin coin)
		{
			Name = name;
			Coin = coin;
		}

		public Name Name { get; }

		public Coin Coin { get; private set; }

		public void AnalyzeAnswer(Answer answer)
		{
			if (answer.Value == 7)
				BadAnswer();
			else
				GoodAnswer();
		}

		private void BadAnswer()
		{
			Coin = new Coin(Coin.Value - 1);
			Domains.RaiseEvent(new BadAnswered());

			if (Coin.Value <= 0)
				Domains.RaiseEvent(new PlayerLoosed());
		}

		private void GoodAnswer()
		{
			Coin = new Coin(Coin.Value + 2);
			Domains.RaiseEvent(new GoodAnswered());

			if (Coin.Value >= 6)
				Domains.RaiseEvent(new PlayerWinned());
		}
	}

	public record Answer(int Value);
	public class PlayerAddErrorOccured : IDomainEvent { }
	public class PlayerAdded : IDomainEvent { public Player4 Player4 { get; set; } }
	public class PlayerWinned : IDomainEvent { }
	public class PlayerLoosed : IDomainEvent { }
	public class GoodAnswered : IDomainEvent { }
	public class BadAnswered : IDomainEvent { }
	#endregion
}
