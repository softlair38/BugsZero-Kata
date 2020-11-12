namespace Trivia.Domain.Events
{
	public readonly struct PlayerRollResponse : IDomainResponse
	{
		public PlayerRollResponse(Roll roll)
		{
			Roll = roll;
		}

		internal Roll Roll { get; }
	}
}
