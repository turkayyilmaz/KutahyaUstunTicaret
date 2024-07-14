namespace KutahyaUstunTicaret.Models
{
	public class ProductViewModel
	{
		public int productId { get; set; }
		public string productName { get; set; }
		public string? description { get; set; }
		public string? image { get; set; }
		public string? unit { get; set; }
		public bool stock { get; set; }
		public string category { get; set; }
		public string brand { get; set; }
	}
}
