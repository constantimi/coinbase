
using Microsoft.EntityFrameworkCore;
using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // In memory database used for simplicity, change to a real db for production applications
            optionsBuilder.UseInMemoryDatabase("DbContext");
        }
    }
}
