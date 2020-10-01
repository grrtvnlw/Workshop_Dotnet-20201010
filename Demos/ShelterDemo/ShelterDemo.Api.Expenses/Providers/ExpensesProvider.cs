using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShelterDemo.Api.Expenses.Db;
using ShelterDemo.Api.Expenses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Expenses.Providers
{
    public class ExpensesProvider : IExpensesProvider
    {
        private readonly ExpensesDbContext dbContext;
        private readonly ILogger<ExpensesProvider> logger;
        private readonly IMapper mapper;

        public ExpensesProvider(ExpensesDbContext dbContext, ILogger<ExpensesProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Expense> Expenses, string ErrorMessage)> GetExpensesAsync(Guid dogId)
        {
            try
            {
                logger?.LogInformation("Querying expenses");

                var expenses = await dbContext.Expenses.Where(expense => expense.DogId == dogId).ToListAsync();
                
                if (expenses != null && expenses.Any())
                {
                    logger?.LogInformation($"{expenses.Count} expense(s) found");

                    var result = mapper.Map<IEnumerable<Db.Expense>, IEnumerable<Models.Expense>>(expenses);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Expenses.Any())
            {
                dbContext.Expenses.Add(new Expense()
                { 
                    Id = Guid.Parse("c6ad8e0f-15e6-4df9-8a19-b50e5d105f76"),
                    DogId = Guid.Parse("005c68b6-a5e9-4fff-b7d9-c88ca27c39f9"),
                    ExpenseDate = DateTime.Now,
                    Total = 100
                });
                dbContext.Expenses.Add(new Expense()
                {
                    Id = Guid.Parse("1976fd50-46cc-40af-8aa4-715c855d8b93"),
                    DogId = Guid.Parse("005c68b6-a5e9-4fff-b7d9-c88ca27c39f9"),
                    ExpenseDate = DateTime.Now.AddDays(-1),
                    Total = 200
                });
                dbContext.Expenses.Add(new Expense()
                {
                    Id = Guid.Parse("695ff5f6-15da-4bf5-8d9d-d8e08d118c7c"),
                    DogId = Guid.Parse("3f229d19-3820-4b1c-afe7-fce462ccaf7f"),
                    ExpenseDate = DateTime.Now,
                    Total = 85
                });

                dbContext.SaveChanges();
            }
        }
    }
}
