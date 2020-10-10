using Microsoft.EntityFrameworkCore;

namespace ShelterDemo.Api.Expenses.Db
{
    public class ExpensesDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpensesDbContext(DbContextOptions options) : base(options) { }
    }
}
