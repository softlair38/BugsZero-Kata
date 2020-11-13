using Trivia.Domain.Events;

namespace Trivia
{
	public class Game
	{
		private Questions<Category> Questions { get; } = new Questions<Category>();

		private RollingList<Player> Players { get; }

		private Player CurrentPlayer => Players.Current;

		private const ushort MinPlayers = 2;
		private const ushort MaxPlayers = 6;
		internal const ushort NbPurseToWin = 6;
		internal const ushort NbPlaces = 12;

		public static void StartNewGame(params Player[] players)
		{
			if (players == null || players.Length < MinPlayers || players.Length > MaxPlayers)
			{
				Domains.RaiseEvent(new GameErrorOccured($"Le nombre de joueur doit être compris entre {MinPlayers} et {MaxPlayers}"));
				return;
			}

			var game = new Game(players);

			Domains.RaiseEvent(new GameStarted(game));
			Domains.RaiseRequest(new PlayerRollRequested(game, game.CurrentPlayer));
		}

		private Game(params Player[] players)
		{
			int number = 0;
			foreach (Player player in players)
			{
				number++;
				Add(player, number);
			}

			Players = new RollingList<Player>(players);
		}

		private void Add(Player player, int number)
		{
			player.ResetGame(this);
			Domains.RaiseEvent(new PlayerAddedToGame(this, player, number));
		}

		internal void AskQuestion()
		{
			Category category = CurrentCategory(CurrentPlayer.Place);
			Domains.RaiseRequest(new PlayerResponseRequested(this, CurrentPlayer, Questions.GetNewOne(category), category.ToString()));
		}

		private Category CurrentCategory(int place)
		{
			return (Category)(place % Questions.NbCategories);
		}

		internal void NextPlayer()
		{
			Players.Next();
			Domains.RaiseRequest(new PlayerRollRequested(this, CurrentPlayer));
		}
	}
}
