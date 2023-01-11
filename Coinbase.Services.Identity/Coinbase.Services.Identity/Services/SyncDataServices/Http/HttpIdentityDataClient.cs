using Coinbase.Services.Identity.Models;
using System.Text.Json;
using System.Text;

namespace Coinbase.Services.Identity.Services.SyncDataServices.Http
{
    public class HttpIdentityDataClient : IIdentityDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpIdentityDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendIdentityToCoinbase(CoinbaseOwnerResponse response)
        {
            StringContent httpContent = new(
                JsonSerializer.Serialize(response),
                Encoding.UTF8,
                "application/json");

            await _httpClient.PostAsync(_configuration["CoinbaseServiceUrl"], httpContent);
        }
    }
}
