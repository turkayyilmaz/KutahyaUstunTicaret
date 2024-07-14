using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Gallery")]
	public class GalleryController : Controller
	{
		private readonly UstunTicaretDbContext _context;
		public GalleryController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index()
		{
			var values = _context.Galleries.ToList();
			return View(values);
		}
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(Gallery model, IFormFile imageFile)
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
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", randomFileName);
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}
					model.ImageName = randomFileName;
					_context.Galleries.Add(model);
					_context.SaveChanges();
					return RedirectToAction("Index", "Gallery", new { area = "admin"});
				}
			}
			return View(model);
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				var value = _context.Galleries.FirstOrDefault(x => x.GalleryId == id);
				_context.Galleries.Remove(value);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Gallery", new { area = "admin" });
			}
			return NotFound();
		}
	}
}
