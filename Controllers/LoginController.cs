using DMS_Hino.Data;
using DMS_Hino.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly DatabaseContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginController(DatabaseContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    // Menampilkan halaman login
    [HttpGet]
    public IActionResult LoginPage()
    {
        return View();
    }

    // Menangani proses login
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            ViewBag.ErrorMessage = "Username tidak ditemukan";
            return View("LoginPage");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            ViewBag.ErrorMessage = "Password salah";
            return View("LoginPage");
        }

        // Jika login sukses, autentikasi user (menggunakan cookie authentication)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Login");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal);

        return RedirectToAction("Index", "Home");  // Arahkan ke halaman utama setelah login sukses
    }

    // Fungsi logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("LoginPage");
    }
}
