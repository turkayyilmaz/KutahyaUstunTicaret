using KutahyaUstunTicaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KutahyaUstunTicaret.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	[Route("Admin/Message")]
	public class MessageController : Controller
	{
		int pageSize = 4;
		private readonly UstunTicaretDbContext _context;
		public MessageController(UstunTicaretDbContext context)
		{
			_context = context;
		}
		[Route("Index")]
		public IActionResult Index(int page = 1)
		{
			var query = _context.Messages.OrderBy(x => x.IsRead).AsQueryable();
			var total = query.Count();



			query = query.Skip((page - 1) * pageSize);
			var values = query.Select(x => new MessageViewModel
			{
				MessageId = x.MessageId,
				FullName = x.FullName,
				EMail = x.EMail,
				Title = x.Title,
				MessageContent = x.MessageContent,
				IsRead = x.IsRead
			}).Take(pageSize).ToList();
			return View(new MessageListViewModel
			{
				Messages = values,
				PageInfo = new PageInfo
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = total
				}
			});
		}
		[Route("Detail")]
		public IActionResult Detail(int? id)
		{
			var value = _context.Messages.FirstOrDefault(x => x.MessageId == id);
			if (value != null)
			{
				value.IsRead = true;
				_context.SaveChanges();
			}
			else
			{
				return RedirectToAction("Index", "Message", new { area = "Admin" });
			}
			return View(value);
		}
		[Route("Delete")]
		public IActionResult Delete(int? id)
		{
			var value = _context.Messages.FirstOrDefault(x => x.MessageId == id);
			if (value != null)
			{
				_context.Messages.Remove(value);
				_context.SaveChanges();
				return RedirectToAction("Index", "Message", new { area = "Admin" });
			}
			return NotFound();
		}
	}
}
