using Microsoft.Extensions.Logging;
using ShelterDemo.Api.Search.Interfaces;
using ShelterDemo.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ExpensesService> logger;

        public ExpensesService(IHttpClientFactory httpClientFactory, ILogger<ExpensesService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Expense> Expenses, string ErrorMessage)> GetExpensesAsync(Guid dogId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ExpensesService");
                var response = await client.GetAsync($"api/expenses/{dogId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Expense>>(content, options);

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
