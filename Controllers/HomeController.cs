using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KutahyaUstunTicaret.Controllers
{
	public class HomeController : Controller
	{
		int pageSize = 16;
		private readonly UstunTicaretDbContext _context;
		public HomeController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Contact()
		{
			ViewBag.v = _context.TopLines.FirstOrDefault();
			ViewBag.s = _context.SocialMedias.ToList();
			return View();
		}
		[HttpPost]
		public IActionResult Contact(Message model)
		{
			ViewBag.v = _context.TopLines.FirstOrDefault();
			ViewBag.s = _context.SocialMedias.ToList();
			if (model == null)
			{
				return NotFound();
			}
			ModelState.Remove(nameof(model.IsRead));
			if (ModelState.IsValid)
			{
				_context.Messages.Add(model);
				_context.SaveChanges();
				TempData["Basarili"] = "Mesaj Ýletildi.";
				return RedirectToAction("Contact", "Home");
			}
			return View();
		}
		public IActionResult About()
		{
			var value = _context.Abouts.FirstOrDefault();
			return View(value);
		}
		public IActionResult Gallery()
		{
			var values = _context.Galleries.ToList();
			return View(values);
		}
		public IActionResult Product(string search, string category, string brand, int page = 1)
		{
			var query = _context.Products.AsQueryable(); //bu çok önemli sorgulanabilir olmalý
			if (!string.IsNullOrEmpty(search))
			{
				ViewBag.Search = search;
				ViewData["Search"] = search;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.ProductName.ToLower().Contains(search.ToLower()));
			}
			if (!string.IsNullOrEmpty(category))
			{
				ViewData["Category"] = category;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.Category.CategoryName == category);
			}
			if (!string.IsNullOrEmpty(brand))
			{
				ViewData["Brand"] = brand;
				query = query.Include(x => x.Brand).Include(x => x.Category).Where(x => x.Brand.BrandName == brand);
			}

			var total = query.Count();
			query = query.Skip((page - 1) * pageSize);
			var values = query
				.Select(x => new ProductViewModel
				{
					image = x.Image,
					brand = x.Brand.BrandName,
					category = x.Category.CategoryName,
					productName = x.ProductName,
					stock = x.Stock,
					unit = x.Unit
				}).Take(pageSize).ToList();
			ViewBag.Categories = _context.Categories.ToList();
			ViewBag.Brands = _context.Brands.ToList();


			return View(new ProductListViewModel
			{
				Products = values,
				PageInfo = new PageInfo
				{
					TotalItems = total,
					ItemsPerPage = pageSize,
					CurrentPage = page
				}
			});


		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
