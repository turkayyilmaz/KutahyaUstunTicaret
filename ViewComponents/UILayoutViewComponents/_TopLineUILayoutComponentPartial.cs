using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _TopLineUILayoutComponentPartial : ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _TopLineUILayoutComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewBag.TopLines = _context.TopLines.FirstOrDefault();
			ViewBag.SocialMedias = _context.SocialMedias.ToList();
			return View();
		}
	}
}
