using System.Text.Json.Serialization;

namespace DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners
{
    public class WinnerResponse
    {
        [JsonPropertyName("elderly")]
        public ParticipantResponse Elderly { get; set; }

        [JsonPropertyName("physicallyHandicapped")]
        public ParticipantResponse PhysicallyHandicapped { get; set; }

        [JsonPropertyName("general")]
        public IEnumerable<ParticipantResponse> General { get; set; }
    }
}