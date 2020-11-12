namespace Trivia.Domain.Events
{
	public readonly struct PlayerRollResponse : IDomainResponse
	{
		public PlayerRollResponse(Roll roll, PlayerRollRequested request)
		{
			Roll = roll;
			Request = request;
		}

		internal Roll Roll { get; }

		internal PlayerRollRequested Request { get; }
	}
}
