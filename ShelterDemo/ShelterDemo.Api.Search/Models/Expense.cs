using System;

namespace ShelterDemo.Api.Search.Models
{
    public class Expense
    {
        public Guid Id { get; set; }

        public Guid DogId { get; set; }

        public DateTime ExpenseDate { get; set; }

        public decimal Total { get; set; }
    }
}
