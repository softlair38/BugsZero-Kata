namespace Trivia.Domain.Events
{
	public readonly struct GameErrorOccured : IDomainEvent
	{
		public string Reason { get; }

		internal GameErrorOccured(string reason)
		{
			Reason = reason;
		}
	}
}