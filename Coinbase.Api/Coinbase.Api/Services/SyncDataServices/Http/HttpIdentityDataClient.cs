using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Coinbase.Api.Models;

namespace Coinbase.Api.Services.SyncDataServices.Http
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
        
        public async Task SendCoinbaseToIdentity(OwnerResponse ownerResponse)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(ownerResponse),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_configuration["ApplicationUrl"], httpContent);
        }
    }
}