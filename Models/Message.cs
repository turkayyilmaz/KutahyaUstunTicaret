using System.ComponentModel.DataAnnotations;

namespace KutahyaUstunTicaret.Models
{
	public class Message
	{
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Ad ve Soyad alanı boş bırakılamaz.")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "E-Posta alanı boş bırakılamaz.")]
		[EmailAddress(ErrorMessage = "Hatalı E-Posta girişi.")]
		public string EMail { get; set; }
        [Required(ErrorMessage = "Konu Başlığı alanı boş bırakılamaz.")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Konu Başlığı alanına en az 5, en fazla 50 karakter girilebilir.")]
		public string Title { get; set; }
        [Required(ErrorMessage = "Mesaj alanı boş bırakılamaz.")]
		[MinLength(10, ErrorMessage = "Mesaj alanına en az 10 karakter girilebilir.")]
        public string MessageContent { get; set; }
		public bool IsRead { get; set; }
    }
}
