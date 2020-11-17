namespace Trivia.Domain.Events
{
	public record GameErrorOccured : IDomainEvent
	{
		public string Reason { get; init; }

		internal GameErrorOccured(string reason)
			=> (Reason) = (reason);
	}
}
