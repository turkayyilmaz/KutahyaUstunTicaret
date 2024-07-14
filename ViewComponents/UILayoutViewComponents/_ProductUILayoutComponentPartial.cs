using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _ProductUILayoutComponentPartial : ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _ProductUILayoutComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var values = _context.Products.Include(x => x.Brand).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
			return View(values);
		}
	}
}
