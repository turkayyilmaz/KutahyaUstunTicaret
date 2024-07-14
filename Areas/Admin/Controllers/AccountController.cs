using KutahyaUstunTicaret.Areas.Admin.Models;
using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Account")]
	public class AccountController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public AccountController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Login.FirstOrDefault();
			return View(values);
		}
		[Route("Index")]
		[HttpPost]
		public IActionResult Index(Login model)
		{
			if (ModelState.IsValid)
			{
				_context.Login.Update(model);
				_context.SaveChanges();
				return RedirectToAction("Index", "Account", new { area = "Admin" });
			}
			return View(model);
		}
	}
}
