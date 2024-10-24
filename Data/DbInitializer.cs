using DMS_Hino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DMS_Hino.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                // Ensure the database is created
                await context.Database.EnsureCreatedAsync();

                // Check and seed Division
                var division = context.Divisions.FirstOrDefault(d => d.Id == "division1");
                if (division == null)
                {
                    division = new Division
                    {
                        Id = "division2",
                        Name = "Admin Division"
                    };
                    context.Divisions.Add(division);
                }

                // Check and seed Department
                var department = context.Departments.FirstOrDefault(d => d.Id == "department1");
                if (department == null)
                {
                    department = new Department
                    {
                        Id = "department2",
                        Name = "Admin Department",
                        DivisionId = division.Id
                    };
                    context.Departments.Add(department);
                }

                if (!context.Users.Any(u => u.Username == "admin"))
                {
                    var adminUser = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = "admin",
                        Role = "Admin",
                        DivisionId = division.Id,  
                        DepartmentId = department.Id,  
                        Password = new PasswordHasher<User>().HashPassword(null, "admin") 
                    };

                    context.Users.Add(adminUser);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
