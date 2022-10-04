namespace DoorPrize.ApplicationCore.DTOs.Response.Participant
{
    public class WinnerResponse
    {
        public ParticipantResponse Elderly { get; set; }
        public ParticipantResponse PhysicallyHandicapped { get; set; }
        public IEnumerable<ParticipantResponse> General { get; set; }
    }
}