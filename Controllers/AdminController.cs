using DMS_Hino.Data;
using DMS_Hino.Models;
using DMS_Hino.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DMS_Hino.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult AllUsersView()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var users = _context.Users.Include(u => u.Division).Include(u => u.Department).ToList();
            return View("AllUsers", users);
        }

        [HttpGet]
        public IActionResult AddUserView()
        {
            var viewModel = new UserViewModel
            {
                Divisions = _context.Divisions.ToList(),
                Departments = _context.Departments.ToList()
            };

            return View("AddUser", viewModel);
        }

        // Form untuk menambahkan user
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            ////if (ModelState.IsValid)
            ////{
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = model.Username,
                    Password = new PasswordHasher<User>().HashPassword(null, model.Password),  // Hash password
                    Role = model.Role,
                    DivisionId = model.DivisionId,
                    DepartmentId = model.DepartmentId
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("AllUsers");
            //}

            // Jika validasi gagal, kembalikan ke view dengan model yang sama untuk perbaikan
            model.Divisions = _context.Divisions.ToList();
            model.Departments = _context.Departments.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddDivisionView()
        {
            return View("AddDivision");
        }

        // Add Division Logic
        [HttpPost]
        public async Task<IActionResult> AddDivision(Division division)
        {
            if (ModelState.IsValid)
            {
                division.Id = Guid.NewGuid().ToString();
                _context.Divisions.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction("AllDivisions");
            }
            return View(division);
        }

        // View for adding a Department
        [HttpGet]
        public IActionResult AddDepartmentView()
        {
            var viewModel = new DepartmentViewModel
            {
                Divisions = _context.Divisions.ToList()
            };
            return View("AddDepartment", viewModel);
        }

        // Add Department Logic
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                department.Id = Guid.NewGuid().ToString();
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("AllDepartments");
            }
            var viewModel = new DepartmentViewModel
            {
                Divisions = _context.Divisions.ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AllDivisionsAndDepartments()
        {
            var viewModel = new DivisionDepartmentViewModel
            {
                Divisions = _context.Divisions.ToList(),
                Departments = _context.Departments.Include(d => d.Division).ToList()
            };

            return View("AllDivDept", viewModel);
        }

        // GET: Display the user detail page with edit option
        [HttpGet]
        public async Task<IActionResult> UserDetail(string id)
        {
            var user = await _context.Users
                .Include(u => u.Division)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                DivisionId = user.DivisionId,
                DepartmentId = user.DepartmentId,
                Division = user.Division,
                Department = user.Department,
                Divisions = await _context.Divisions.ToListAsync(),
                Departments = await _context.Departments.ToListAsync()
            };

            return View(model);
        }


        // POST: Save the edited user data
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel model)
        {
            // Validate model before saving changes
            //if (ModelState.IsValid)
            //{
                // Retrieve the user to update
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
                if (user == null)
                {
                    return NotFound(); // Return 404 if user not found
                }

                // Update user properties with new values from the form
                user.Username = model.Username;
                user.Role = model.Role;
                user.DivisionId = model.DivisionId;
                user.DepartmentId = model.DepartmentId;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect back to detail view after saving
                return RedirectToAction("UserDetail", new { id = model.Id });
            //}

            // If model validation fails, reload the page with division and department data
            model.Divisions = await _context.Divisions.ToListAsync();
            model.Departments = await _context.Departments.ToListAsync();

            return View("UserDetail", model); // Reload the form with current data
        }
    }
}
