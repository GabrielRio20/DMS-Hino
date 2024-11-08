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

                if (!context.Users.Any(u => u.Username == "admin"))
                {
                    var adminDivision = await context.Divisions.FindAsync("AdminDivision");
                    if (adminDivision == null)
                    {
                        adminDivision = new Division { Id = "AdminDivision", Name = "Admin Division" };
                        context.Divisions.Add(adminDivision);
                    }

                    var adminDepartment = await context.Departments.FindAsync("AdminDepartment");
                    if (adminDepartment == null)
                    {
                        adminDepartment = new Department { Id = "AdminDepartment", Name = "Admin Department", DivisionId = adminDivision.Id };
                        context.Departments.Add(adminDepartment);
                    }

                    var adminUser = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = "admin",
                        Role = "Admin",
                        DivisionId = "AdminDivision",  
                        DepartmentId = "AdminDepartment",  
                        Password = new PasswordHasher<User>().HashPassword(null, "admin") 
                    };

                    context.Users.Add(adminUser);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
