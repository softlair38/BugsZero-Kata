namespace Trivia.Domain.Events
{
	public readonly struct PlayerResponseResponse : IDomainResponse
	{
		public PlayerResponseResponse(int response)
		{
			Response = response;
		}

		internal int Response { get; }
	}
}
