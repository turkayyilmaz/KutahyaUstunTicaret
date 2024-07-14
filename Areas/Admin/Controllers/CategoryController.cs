using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Category")]
	public class CategoryController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public CategoryController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Categories.ToList();
			return View(values);
		}
		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(Category model)
		{
			if (model != null)
			{
				_context.Categories.Add(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category", new { area = "Admin" });

			}
			return NotFound();
		}
		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var value = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
			return View(value);
		}
		[HttpPost]
		[Route("Edit")]
		public async Task<IActionResult> Edit(Category model)
		{
			if (model != null)
			{
				_context.Categories.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category", new { area = "Admin" });

			}
			return NotFound();
		}
		[HttpGet]
		[Route("Delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var value = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
			return View(value);
		}
		[HttpPost]
		[Route("Delete")]
		public async Task<IActionResult> Delete(Category model)
		{
			if (model.CategoryId != null)
			{
				var value = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId ==model.CategoryId);
				_context.Categories.Remove(value);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category", new { area = "Admin" });

			}
			return NotFound();
		}
	}
}
