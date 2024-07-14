using System.ComponentModel.DataAnnotations;

namespace KutahyaUstunTicaret.Areas.Admin.Models
{
    public class Login
    {
        public int LoginId { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
