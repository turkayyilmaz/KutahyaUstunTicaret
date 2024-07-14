using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _NavbarUILayoutComponentPartial : ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _NavbarUILayoutComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewBag.TopLines = _context.TopLines.FirstOrDefault();
			ViewBag.Logo = _context.Logo.FirstOrDefault();
			return View();
		}
	}
}
