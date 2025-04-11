using EFCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWebAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Look for any departments.
                if (context.Departments.Any())
                {
                    return;   // DB has been seeded
                }

                var softwareDev = new Department { Name = "Software Development" };
                var finance = new Department { Name = "Finance" };
                var accounting = new Department { Name = "Accountant" };
                var hr = new Department { Name = "HR" };

                context.Departments.AddRange(
                    softwareDev,
                    finance,
                    accounting,
                    hr
                );
                
                context.SaveChanges();
            }
        }
    }
}