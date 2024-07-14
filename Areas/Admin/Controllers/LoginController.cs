using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KutahyaUstunTicaret.Areas.Admin.Models;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Login")]
	public class LoginController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public LoginController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			return View();
		}
		[HttpPost]
		[Route("Index")]
		public async Task<IActionResult> Index(Login model)
		{
			if (ModelState.IsValid)
			{
				var user = _context.Login.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

				if (user != null)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.Username)
                        // İhtiyaç duyulan diğer claimler buraya eklenebilir
                    };

					var claimsIdentity = new ClaimsIdentity(
						claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var authProperties = new AuthenticationProperties
					{
						// Varsayılan auth properties burada ayarlanabilir
					};

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties);

					return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre"); //bu güzelmiş
				}
			}
			return View(model);
		}
	}
}
