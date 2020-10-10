using Microsoft.EntityFrameworkCore;

namespace ShelterDemo.Api.Dogs.Db
{
    public class DogsDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsDbContext(DbContextOptions options) : base(options) { }
    }
}
