using System.Text.Json.Serialization;

namespace DoorPrize.ApplicationCore.DTOs.Response.Participant.Get
{
    public class GetResponse
    {
        [JsonPropertyName("elderly")]
        public IEnumerable<ParticipantResponse> Elderly { get; set; }

        [JsonPropertyName("physicallyHandicapped")]
        public IEnumerable<ParticipantResponse> PhysicallyHandicapped { get; set; }

        [JsonPropertyName("general")]
        public IEnumerable<ParticipantResponse> General { get; set; }

        [JsonPropertyName("numberParticipants")]
        public int NumberParticipants { get; set; }
    }
}