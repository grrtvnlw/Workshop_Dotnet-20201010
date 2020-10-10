using ShelterDemo.Api.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShelterDemo.Api.Expenses.Interfaces
{
    public interface IExpensesProvider
    {
        Task<(bool IsSuccess, IEnumerable<Expense> Expenses, string ErrorMessage)> GetExpensesAsync(Guid dogId);
    }
}
