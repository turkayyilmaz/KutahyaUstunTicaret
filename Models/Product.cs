using System.ComponentModel.DataAnnotations;

namespace KutahyaUstunTicaret.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		//public double Price { get; set; }
		public string? Unit { get; set; }
		//public double? Kdv{ get; set; }
		public bool Stock { get; set; }
		[Required(ErrorMessage = "Kategori seçiniz!")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public int BrandId{ get; set; }
		public Brand Brand { get; set; }
	}
}
