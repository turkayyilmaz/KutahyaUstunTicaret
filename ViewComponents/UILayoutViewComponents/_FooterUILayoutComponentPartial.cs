using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.ViewComponents.UILayoutViewComponents
{
	public class _FooterUILayoutComponentPartial : ViewComponent
	{
		private readonly UstunTicaretDbContext _context;
		public _FooterUILayoutComponentPartial(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			ViewBag.TopLines = _context.TopLines.FirstOrDefault();
			ViewBag.SocialMedias = _context.SocialMedias.ToList();
			ViewBag.Categories = _context.Categories.ToList();
			return View();
		}
	}
}
