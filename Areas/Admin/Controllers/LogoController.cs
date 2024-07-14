using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Logo")]
	public class LogoController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public LogoController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var value = await _context.Logo.FirstOrDefaultAsync();
			return View(value);
		}
		[Route("Index")]
		[HttpPost]
		public async Task<IActionResult> Index(Logo model,IFormFile imageFile)
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
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Logo", randomFileName);
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}
					model.Image = randomFileName;
					_context.Logo.Update(model);
					_context.SaveChanges();
					return RedirectToAction("Index", "Logo", new { area = "admin" });
				}
			}
			return View(model);
		}
	}
}
