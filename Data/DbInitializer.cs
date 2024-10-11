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

                await SeedDocuments(context);
            }
        }

        private static async Task SeedDocuments(DatabaseContext context)
        {
            // Check if any documents exist
            if (!context.Documents.Any())
            {
                var documents = new List<Document>
            {
                new Document
                {
                    Id = "doc1",
                    DocumentItem = "Legal Agreement",
                    Number = "LA-2024-01",
                    Name = "Service Agreement",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 1, 15),
                    Tag = "Contract",
                    Location = "Locker A-1",
                    IsPublic = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = "admin" // Ganti sesuai dengan ID pengguna yang valid
                },
                new Document
                {
                    Id = "doc2",
                    DocumentItem = "Project Plan",
                    Number = "PP-2024-02",
                    Name = "2024 Project Plan",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 2, 10),
                    Tag = "Planning",
                    Location = "Locker A-2",
                    IsPublic = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = "admin" // Ganti sesuai dengan ID pengguna yang valid
                },
                // Tambahkan lebih banyak dokumen di sini
                new Document
                {
                    Id = "doc3",
                    DocumentItem = "Financial Report",
                    Number = "FR-2024-03",
                    Name = "Q1 Financial Report",
                    VersionName = "v1.0",
                    ReleasedDate = new DateTime(2024, 3, 20),
                    Tag = "Finance",
                    Location = "Locker A-3",
                    IsPublic = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = "admin" // Ganti sesuai dengan ID pengguna yang valid
                },
                // Lanjutkan dengan lebih banyak dokumen...
            };

                await context.Documents.AddRangeAsync(documents);
                await context.SaveChangesAsync(); // Pastikan untuk menyimpan perubahan
            }
        }
    }

}
