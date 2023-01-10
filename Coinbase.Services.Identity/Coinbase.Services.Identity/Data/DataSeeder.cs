using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Coinbase.Services.Identity.Data
{
    public static class DataSeeder
    {
        public static void CreateData(IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<DataContext>());
            }
        }

        private static void SeedData(DataContext context)
        {
            List<Owner> testOwners = new()
            {
                new Owner { FirstName = "Admin", LastName = "Owner", Email="admin@mail.com", Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Role = Role.Admin },
                new Owner { FirstName = "Normal", LastName = "Owner",Email="Owner@mail.com", Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Role = Role.User }
            };

            context.Owners.AddRange(testOwners);
            context.SaveChanges();
        }
    }
}
