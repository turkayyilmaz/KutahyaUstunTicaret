using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Brand")]
	public class BrandController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public BrandController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Brands.ToList();
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
		public async Task<IActionResult> Create(Brand model)
		{
			if (model != null)
			{
				_context.Brands.Add(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Brand", new { area = "Admin" });

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
			var value = _context.Brands.FirstOrDefault(x => x.BrandId == id);
			return View(value);
		}
		[HttpPost]
		[Route("Edit")]
		public async Task<IActionResult> Edit(Brand model)
		{
			if (model != null)
			{
				_context.Brands.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Brand", new { area = "Admin" });

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
			var value = _context.Brands.FirstOrDefault(x => x.BrandId == id);
			return View(value);
		}
		[HttpPost]
		[Route("Delete")]
		public async Task<IActionResult> Delete(Brand model)
		{
			if (model.BrandId != null)
			{
				var value = await _context.Brands.FirstOrDefaultAsync(x => x.BrandId == model.BrandId);
				_context.Brands.Remove(value);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Brand", new { area = "Admin" });

			}
			return NotFound();
		}
	}
}
