namespace Trivia.Domain.Events
{
	public record GameErrorOccured : IDomainEvent
	{
		public string Reason { get; }

		internal GameErrorOccured(string reason)
			=> (Reason) = (reason);
	}
}
