using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Dashboard")]
	public class DashboardController : Controller
	{
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
