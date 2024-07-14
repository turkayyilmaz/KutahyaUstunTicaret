using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/TopLine")]
	public class TopLineController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public TopLineController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Iletisim")]
		[HttpGet]
		public IActionResult Iletisim()
		{
			var values = _context.TopLines.FirstOrDefault();
			return View(values);
		}
		[Route("Iletisim")]
		[HttpPost]
		public IActionResult Iletisim(TopLine model)
		{
			_context.TopLines.Update(model);
			_context.SaveChanges();
			return RedirectToAction("Iletisim", "TopLine");
		}
	}
}
