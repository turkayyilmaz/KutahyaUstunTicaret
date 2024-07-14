using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _DealerUILayoutComponentPartial :ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _DealerUILayoutComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var values = _context.Dealerships.ToList();
			return View(values);
		}
	}
}
