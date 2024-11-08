using DMS_Hino.Data;
using DMS_Hino.Models;
using DMS_Hino.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DMS_Hino.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult DashboardAdmin()
        {
            return View();
        }

        public IActionResult AllUsersView()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var users = _context.Users.Include(u => u.Division).Include(u => u.Department).ToList();
            return View("AllUsers", users);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserView()
        {
            var viewModel = new UserViewModel
            {
                Divisions = await _context.Divisions.ToListAsync(),
                Departments = await _context.Departments.ToListAsync()
            };

            return View("AddUser", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var isTaken = await _context.Users.AnyAsync(u => u.Username == username);
            return Json(new { isTaken });
        }

        // Form untuk menambahkan user
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);  // Return the view with validation error
            }

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

            // Ambil semua user dari database
            var allUsers = await _context.Users.ToListAsync();

            // Kembalikan list user ke view AllUsers
            //return View("AllUsersView", allUsers);
            return RedirectToAction("AllUsersView", "Admin");

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

        public IActionResult AddDivDept()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDivisionWithDepartments(DivisionDepartmentViewModel model)
        {
            foreach (var division in model.Divisions)
            {
                if (string.IsNullOrEmpty(division.Id))
                {
                    division.Id = Guid.NewGuid().ToString();  
                }

                _context.Divisions.Add(division);
                _context.SaveChanges();  

                foreach (var department in model.Departments)
                {
                    if (string.IsNullOrEmpty(department.Id))
                    {
                        department.Id = Guid.NewGuid().ToString();  
                    }

                    department.DivisionId = division.Id;
                    _context.Departments.Add(department);  
                }

                _context.SaveChanges();
            }

            return RedirectToAction("AllDivisionsAndDepartments", "Admin");
        }

        public async Task<IActionResult> UpdateDivisionName(string id, string newName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newName))
            {
                return BadRequest("ID atau nama baru tidak valid.");
            }

            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
            {
                return NotFound("Divisi tidak ditemukan.");
            }

            division.Name = newName;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(); // Status 200 jika berhasil
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartmentName(string id, string newName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newName))
            {
                return BadRequest("ID atau nama baru tidak valid.");
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound("Departemen tidak ditemukan.");
            }

            department.Name = newName;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(); 
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddDepartmentLive(string divisionId, string departmentName)
        {
            if (string.IsNullOrEmpty(departmentName))
            {
                return Json(new { success = false, message = "Department name cannot be empty" });
            }

            var newDepartment = new Department
            {
                Id = Guid.NewGuid().ToString(), // Ensure a unique ID is set
                DivisionId = divisionId,
                Name = departmentName
            };

            _context.Departments.Add(newDepartment);
            _context.SaveChanges();

            return Json(new { success = true, departmentId = newDepartment.Id, departmentName = newDepartment.Name });
        }

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

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = model.Username;
            user.Role = model.Role;
            user.DivisionId = model.DivisionId;
            user.DepartmentId = model.DepartmentId;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = new PasswordHasher<User>().HashPassword(user, model.Password);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("UserDetail", new { id = model.Id });
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = "Invalid user ID.";
                return RedirectToAction("AllUsersView");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                return RedirectToAction("AllUsersView");
            }

            foreach (var document in user.CreatedDocuments)
            {
                document.CreatedBy = null;  
            }

            foreach (var document in user.ModifiedDocuments)
            {
                document.ModifiedBy = null;  
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error deleting user: " + ex.Message;
            }

            return RedirectToAction("AllUsersView");
        }
    }
}
