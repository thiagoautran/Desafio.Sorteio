using System.Text.Json.Serialization;

namespace DoorPrize.ApplicationCore.DTOs.Response.Participant
{
    public class ParticipantResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }
    }
}