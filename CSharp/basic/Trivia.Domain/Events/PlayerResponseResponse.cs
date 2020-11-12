namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseResponse : IDomainResponse
	{
		public PlayerResponseResponse(int response, PlayerResponseRequested request)
		{
			Response = response;
			Request = request;
		}

		internal int Response { get; }

		internal PlayerResponseRequested Request { get; }
	}
}
