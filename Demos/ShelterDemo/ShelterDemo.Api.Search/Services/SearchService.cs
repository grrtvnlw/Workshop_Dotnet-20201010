using ShelterDemo.Api.Search.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDogsService dogsService;
        private readonly IExpensesService expensesService;

        public SearchService(IDogsService dogsService, IExpensesService expensesService)
        {
            this.dogsService = dogsService;
            this.expensesService = expensesService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(Guid dogId)
        {
            var dogResult = await dogsService.GetDogAsync(dogId);
            var expensesResult = await expensesService.GetExpensesAsync(dogId);

            if (expensesResult.IsSuccess)
            {
                var result = new
                {
                    Dog = dogResult.IsSuccess ? dogResult.Dog : new { Name = "Dog information is not available" },
                    Expenses = expensesResult.Expenses
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
