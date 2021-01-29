using Trivia.Domain.Events.Base;
using Trivia.Domain.ValueType;

namespace Trivia.Domain.Events.Players
{
	public record PlayerResponseRequested : IDomainRequest
	{
		public Player Player { get; }
		public Question Question { get; }

		internal PlayerResponseRequested(Player player, Question question)
			=> (Player, Question) = (player, question);

		public void Response(Response response)
		{
			if (Question.IsGoodResponse(response))
				Player.WrongAnswer();
			else
				Player.WasCorrectlyAnswered();
		}
	}
}
