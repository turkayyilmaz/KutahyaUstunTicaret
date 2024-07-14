namespace KutahyaUstunTicaret.Models
{
	public class PageInfo
	{
        public int TotalItems { get; set; } // Toplam ürün sayısı.
        public int ItemsPerPage { get; set; } // Sayfa başına ürün sayısı
		public int CurrentPage { get; set; }
		public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); // Sayfa sayısı
    }
}
