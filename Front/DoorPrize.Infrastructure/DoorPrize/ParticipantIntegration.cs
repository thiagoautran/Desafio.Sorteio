using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
using DoorPrize.ApplicationCore.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DoorPrize.Infrastructure.DoorPrize
{
    public class ParticipantIntegration : IParticipantIntegration
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ParticipantIntegration(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<GetResponse> Get()
        {
            using var client = _httpClientFactory.CreateClient("PARTICIPANT");

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri("http://host.docker.internal:8003/door.prize/v1/participant");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);
            var contentString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<GetResponse>(contentString);
            }

            return null;
        }

        public async Task<WinnerResponse> GetWinner()
        {
            using var client = _httpClientFactory.CreateClient("PARTICIPANT");

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri("http://host.docker.internal:8003/door.prize/v1/participant/winners");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);
            var contentString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<WinnerResponse>(contentString);
            }

            return null;
        }
    }
}