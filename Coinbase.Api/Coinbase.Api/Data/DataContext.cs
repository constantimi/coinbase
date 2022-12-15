using Coinbase.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // In memory database used for simplicity, change to a real db for production applications
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
