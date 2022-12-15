using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Coinbase.Services.Identity.Data
{
    public class DataSeeder
    {
        public static void CreateData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<DataContext>());
            }
        }

        private static void SeedData(DataContext context)
        {
            var testOwners = new List<Owner>
            {
                new Owner { Id = 1, FirstName = "Admin", LastName = "Owner", Email="admin@mail.com", Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
                new Owner { Id = 2, FirstName = "Normal", LastName = "Owner",Email="Owner@mail.com", Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.User }
            };

            context.Owners.AddRange(testOwners);
            context.SaveChanges();
        }
    }
}
