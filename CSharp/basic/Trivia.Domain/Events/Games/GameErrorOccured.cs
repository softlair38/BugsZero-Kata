using Trivia.Domain.Events.Base;

namespace Trivia.Domain.Events.Games
{
	public record GameErrorOccured : IDomainEvent
	{
		public string Reason { get; }

		internal GameErrorOccured(string reason)
			=> Reason = reason;
	}
}
