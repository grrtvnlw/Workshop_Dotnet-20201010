using ShelterDemo.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Search.Interfaces
{
    public interface IExpensesService
    {
        Task<(bool IsSuccess, IEnumerable<Expense> Expenses, string ErrorMessage)> GetExpensesAsync(Guid dogId);
    }
}
