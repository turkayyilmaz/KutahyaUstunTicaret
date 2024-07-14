using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _SideCategoryComponentPartial :ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _SideCategoryComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"]; //program.cs'deki routetaki category
			ViewBag.Categories = _context.Categories.ToList();
			return View();
		}
	}
}
