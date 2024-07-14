using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/SocialMedia")]
	public class SocialMediaController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public SocialMediaController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		[HttpGet]
		public IActionResult Index()
		{
			var values = _context.SocialMedias.ToList();
			return View(values);
		}
		[Route("Create")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[Route("Create")]
		[HttpPost]
		public IActionResult Create(SocialMedia model)
		{
			if (model != null)
			{
				_context.SocialMedias.Add(model);
				_context.SaveChanges();
				return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
			}
			return NotFound();
		}
		[Route("Edit")]
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var value = _context.SocialMedias.FirstOrDefault(x => x.SocialMediaId == id);
			return View(value);
		}
		[Route("Edit")]
		[HttpPost]
		public IActionResult Edit(SocialMedia model)
		{
			if (model != null)
			{
				_context.SocialMedias.Update(model);
				_context.SaveChanges();
				return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
			}
			return NotFound();
		}
		[Route("Delete")]
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var value = _context.SocialMedias.FirstOrDefault(x => x.SocialMediaId == id);
			_context.SocialMedias.Remove(value);
			_context.SaveChanges();
			return RedirectToAction("Index", "SocialMedia", new { area = "Admin" });
		}
	}
}
