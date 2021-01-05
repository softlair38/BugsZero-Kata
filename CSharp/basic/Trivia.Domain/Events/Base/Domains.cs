﻿namespace Trivia.Domain.Events.Base
{
	public static class Domains
	{
		public static event DomainTriggered? OnDomainTriggered;

		internal static void RaiseEvent(IDomainEvent domainEvent)
		{
			OnDomainTriggered?.Invoke(domainEvent);
		}

		public static void RaiseRequest(IDomainRequest domainRequest)
		{
			OnDomainTriggered?.Invoke(domainRequest);
		}
	}
}
