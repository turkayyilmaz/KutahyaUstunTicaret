using KutahyaUstunTicaret.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace KutahyaUstunTicaret.Models
{
	public class UstunTicaretDbContext : DbContext
	{
		public UstunTicaretDbContext(DbContextOptions<UstunTicaretDbContext> options) : base(options)
		{

		}
		public DbSet<Navbar> Navbars { get; set; }
		public DbSet<SocialMedia> SocialMedias { get; set; }
		public DbSet<TopLine> TopLines { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<About> Abouts { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Gallery> Galleries { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Dealership> Dealerships { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Logo> Logo { get; set; }
		public DbSet<Login> Login { get; set; }

	}
}
