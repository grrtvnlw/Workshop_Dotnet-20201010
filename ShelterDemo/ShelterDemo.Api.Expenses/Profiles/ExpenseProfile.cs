namespace ShelterDemo.Api.Expenses.Profiles
{
    public class ExpenseProfile : AutoMapper.Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Db.Expense, Models.Expense>();
        }
    }
}
