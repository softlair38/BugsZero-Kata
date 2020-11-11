namespace Trivia.Domain.Events
{
	public static class DomainEvent
	{
		public static event DomainEventTrigered OnDomainEventTriggered;

		internal static void Raise(IDomainEvent domainEvent)
		{
			OnDomainEventTriggered?.Invoke(domainEvent);
		}
	}
}