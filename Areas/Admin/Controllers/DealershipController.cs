using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Dealership")]
	public class DealershipController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public DealershipController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Dealerships.ToList();
			return View(values);
		}
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(Dealership model, IFormFile imageFile)
		{
			if (imageFile != null)
			{
				var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
				var extension = Path.GetExtension(imageFile.FileName); //.jpg, .png etc. we take them.
				if (!allowedExtensions.Contains(extension))
				{
					ModelState.AddModelError("imageFile", "Resim uzantısı bunlar olmalı: .jpg, .jpeg ve .png .");
					return View(model);
				}
				if (imageFile != null)
				{
					var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //we create a new random name and added the extension.
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Dealer", randomFileName);
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}
					model.Image = randomFileName;
					_context.Dealerships.Add(model);
					_context.SaveChanges();
					return RedirectToAction("Index", "Dealership", new { area = "admin" });
				}
			}
			ViewBag.Categories = await _context.Categories.ToListAsync();
			return View(model);
		}
		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id != null)
			{
				var value = await _context.Dealerships.FirstOrDefaultAsync(x => x.DealershipId == id);
				if (value != null)
				{
					return View(value);
				}
				else
				{
					return NotFound();
				}
			}
			return NotFound();
		}
		[HttpPost]
		[Route("Edit")]
		public async Task<IActionResult> Edit(Dealership model, IFormFile? imageFile)
		{
			if (model.DealershipId == 0)
			{
				return NotFound();
			}
			if (imageFile != null)
			{
				var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
				var extension = Path.GetExtension(imageFile.FileName); //.jpg, .png etc. we take them.
				if (!allowedExtensions.Contains(extension))
				{
					ModelState.AddModelError("imageFile", "Resim uzantısı bunlar olmalı: .jpg, .jpeg ve .png .");
					return View(model);
				}

				var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //we create a new random name and added the extension.
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Dealer", randomFileName);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					await imageFile.CopyToAsync(stream);
				}
				model.Image = randomFileName;
			}
			ViewBag.Categories = await _context.Categories.ToListAsync();
			_context.Dealerships.Update(model);
			_context.SaveChanges();
			return RedirectToAction("Index", "Dealership", new { area = "admin" });
		}
		[Route("Delete")]
		public IActionResult Delete(int? id)
		{
			if (id != null)
			{
				var value = _context.Dealerships.FirstOrDefault(x => x.DealershipId == id);
				if (value != null)
				{
					_context.Dealerships.Remove(value);
					_context.SaveChanges();
					return RedirectToAction("Index", "Dealership", new { area = "Admin" });
				}
			}
			return NotFound();
		}
	}
}
