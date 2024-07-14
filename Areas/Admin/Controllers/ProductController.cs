using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Product")]
	public class ProductController : Controller
	{
		public int pageSize = 20; //sayfada görülecek max ürün sayısı
		private readonly UstunTicaretDbContext _context;
		public ProductController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		[Route("Index")]
		public async Task<IActionResult> Index(string category, string brand, string search, int page = 1)
		{
			var query = _context.Products.AsQueryable();
			ViewBag.SelectedCategory = category;
			ViewBag.Categories = _context.Categories.ToList();
			ViewBag.SelectedBrand = brand;
			ViewBag.Brands = _context.Brands.ToList();
			if (!string.IsNullOrEmpty(search) && search != "0")
			{
				ViewBag.Search = search;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.ProductName.ToLower().Contains(search.ToLower()));
			}
			if (!string.IsNullOrEmpty(category) && category != "0")
			{
				ViewData["Category"] = category;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.Category.CategoryName == category);
			}
			if (!string.IsNullOrEmpty(brand) && brand != "0")
			{
				ViewData["Brand"] = brand;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.Brand.BrandName == brand);
			}
			var total = query.Count();
			query = query.Skip((page - 1) * pageSize);

			var values = await query //veritabanındaki ürünlerin verildiği değeri kadar öteler ve sonrasını alır
				.Select(x => new ProductViewModel
				{
					productId = x.ProductId,
					productName = x.ProductName,
					description = x.Description,
					image = x.Image,
					stock = x.Stock,
					unit = x.Unit,
					brand = x.Brand.BrandName,
					category = x.Category.CategoryName
				})
				.Take(pageSize)
				.ToListAsync();
			//yukarıdaki values bir listtir ancak index.cshtml'de model bekliyoruz hata veriri o yüzden bu listeyi listviewe at
			return View(new ProductListViewModel
			{
				Products = values,
				PageInfo = new PageInfo
				{
					TotalItems = total, //valuesin countunu almıştın bir zamanlar unutma :D
					ItemsPerPage = pageSize,
					CurrentPage = page,
				}
			});
		}
		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = await _context.Categories.ToListAsync();
			ViewBag.Brands = await _context.Brands.ToListAsync();
			return View();
		}
		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(Product model, IFormFile imageFile)
		{

			if (imageFile != null)
			{
				var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
				var extension = Path.GetExtension(imageFile.FileName); //.jpg, .png etc. we take them.
				if (!allowedExtensions.Contains(extension))
				{
					ModelState.AddModelError("imageFile", "Resim uzantısı bunlar olmalı: .jpg, .jpeg ve .png .");
					ViewBag.Categories = await _context.Categories.ToListAsync();
					ViewBag.Brands = await _context.Brands.ToListAsync();
					return View(model);
				}
				if (imageFile != null)
				{
					var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //we create a new random name and added the extension.
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Products", randomFileName);
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}
					model.Image = randomFileName;
					_context.Products.Add(model);
					_context.SaveChanges();
					return RedirectToAction("Index", "Product", new { area = "admin" });
				}
			}
			ViewBag.Categories = await _context.Categories.ToListAsync();
			ViewBag.Brands = await _context.Brands.ToListAsync();
			return View(model);

		}
		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id != null)
			{
				var value = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
				if (value != null)
				{
					ViewBag.Categories = await _context.Categories.ToListAsync();
					ViewBag.Brands = await _context.Brands.ToListAsync();
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
		public async Task<IActionResult> Edit(Product model, IFormFile? imageFile)
		{
			if (model.ProductId == 0)
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
					ViewBag.Categories = await _context.Categories.ToListAsync();
					ViewBag.Brands = await _context.Brands.ToListAsync();
					return View(model);
				}

				var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}"); //we create a new random name and added the extension.
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Products", randomFileName);
				using (var stream = new FileStream(path, FileMode.Create))
				{
					await imageFile.CopyToAsync(stream);
				}
				model.Image = randomFileName;
			}
			ViewBag.Categories = await _context.Categories.ToListAsync();
			ViewBag.Brands = await _context.Brands.ToListAsync();
			_context.Products.Update(model);
			_context.SaveChanges();
			return RedirectToAction("Index", "Product", new { area = "admin" });
		}
		[Route("Delete")]
		public IActionResult Delete(int? id)
		{
			if (id != null)
			{
				var value = _context.Products.FirstOrDefault(x => x.ProductId == id);
				if (value != null)
				{
					_context.Products.Remove(value);
					_context.SaveChanges();
					return RedirectToAction("Index", "Product", new { area = "Admin" });
				}
			}
			return NotFound();
		}
	}
}
