namespace KutahyaUstunTicaret.Models
{
	public class Brand
	{
		public int BrandId { get; set; }
		public string BrandName { get; set; }
		public List<Product> Product { get; set; }
	}
}
