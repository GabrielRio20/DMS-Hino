using DMS_Hino.Data;
using DMS_Hino.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("UserId", user.Id)
    };

        var claimsIdentity = new ClaimsIdentity(claims, "Login");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal);

        // Redirect to the default page based on user role
        if (user.Role == "Admin")
        {
            return RedirectToAction("PublicDocument", "Document");
        }
        else if (user.Role == "User")
        {
            return RedirectToAction("PublicDocument", "Document");
        }

        return RedirectToAction("Index", "Home");
    }

    // Fungsi logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("LoginPage");
    }

    
}
