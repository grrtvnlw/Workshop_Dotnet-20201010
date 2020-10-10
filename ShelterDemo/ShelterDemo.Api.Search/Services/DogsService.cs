using Microsoft.Extensions.Logging;
using ShelterDemo.Api.Search.Interfaces;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Services
{
    public class DogsService : IDogsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<DogsService> logger;

        public DogsService(IHttpClientFactory httpClientFactory, ILogger<DogsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic Dog, string ErrorMessage)> GetDogAsync(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("DogsService");
                var response = await client.GetAsync($"api/dogs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
