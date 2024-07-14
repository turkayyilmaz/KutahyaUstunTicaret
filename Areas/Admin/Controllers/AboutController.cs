using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/About")]
	public class AboutController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public AboutController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Abouts.FirstOrDefault();
			return View(values);
		}
		[HttpPost]
		[Route("Index")]
		public IActionResult Index(About model)
		{
			if (model != null)
			{
				_context.Abouts.Update(model);
				_context.SaveChanges();
				return View(model);
			}
			return NotFound();
		}
	}
}
