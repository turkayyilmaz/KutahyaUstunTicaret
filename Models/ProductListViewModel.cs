namespace KutahyaUstunTicaret.Models
{
	//buraya pagination itemleri falan gelecek hepsini aynı razor sayfasında kullanabilmek için oluşturduk bu cs'i
	public class ProductListViewModel
	{
		public List<ProductViewModel> Products { get; set; } = new();
		public PageInfo PageInfo { get; set; } = new();
	}
}
