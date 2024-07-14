using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _SideBrandComponentPartial : ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _SideBrandComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedBrand= RouteData?.Values["brand"]; //program.cs'deki routetaki brand
			ViewBag.Brands = _context.Brands.ToList();
			return View();
		}
	}
}
