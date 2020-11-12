namespace Trivia.Domain.Events
{
	/// <summary>
	/// StartGameRequested(params string[] playerNames) IRequestDomainEvent
	/// GameStartedErrorOccured(string reason) IResponseDomainEvent
	/// GameStarted(Guid gameId) IResponseDomainEvent
	/// PlayerRollRequested(Guid gameId, string playerName) : action : Roll(int number) de 1 à 6 IRequestDomainEvent
	/// PlayerResponseRequested(Guid gameId, string playerName) : action : Answer(int response) de 0 à 9 IRequestDomainEvent
	/// PlayerWentToPenaltyBox(Guid gameId, string playerName) IDomainEvent
	/// PlayerGoOutOfPenaltyBox(Guid gameId, string playerName) IDomainEvent
	/// PlayerGoodResponseSended(Guid gameId, string playerName) IDomainEvent
	/// PlayerBadResponseSended(Guid gameId, string playerName) IDomainEvent
	/// PlayerHasWinned(Guid gameId, string playerName) IDomainEvent
	/// GameEnded(Guid gameId) IDomainEvent
	/// </summary>
	public static class Domains
	{
		public static event DomainTriggered OnDomainTriggered;

		internal static void RaiseEvent(IDomainEvent domainEvent)
		{
			OnDomainTriggered?.Invoke(domainEvent);
		}

		public static void RaiseRequest(IDomainRequest domainRequest)
		{
			OnDomainTriggered?.Invoke(domainRequest);
		}

		public static void RaiseResponse(IDomainResponse domainResponse)
		{
			OnDomainTriggered?.Invoke(domainResponse);
		}
	}
}
