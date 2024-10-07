using DMS_Hino.Data;
using DMS_Hino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DMS_Hino.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult AddUserView()
        {
            //return View("DashboardAdmin");
            return PartialView("AddUser");
        }

        // Form untuk menambahkan user
        [HttpPost]
        public async Task<IActionResult> AddUser(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = username,
                    Password = new PasswordHasher<User>().HashPassword(null, password),  // Hash password
                    Role = "User",  // Set role sebagai User
                    DivisionId = "divisionId",  // Ganti dengan Division ID yang sesuai
                    DepartmentId = "departmentId"  // Ganti dengan Department ID yang sesuai
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}
