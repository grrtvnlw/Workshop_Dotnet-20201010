using Microsoft.AspNetCore.Mvc;
using ShelterDemo.Api.Expenses.Interfaces;
using System;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Expenses.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesProvider expensesProvider;

        public ExpensesController(IExpensesProvider expensesProvider)
        {
            this.expensesProvider = expensesProvider;
        }

        [HttpGet("{dogId}")]
        public async Task<IActionResult> GetExpensesAsync(Guid dogId)
        {
            var result = await expensesProvider.GetExpensesAsync(dogId);

            if (result.IsSuccess)
            {
                return Ok(result.Expenses);
            }
            return NotFound();
        }
    }
}
