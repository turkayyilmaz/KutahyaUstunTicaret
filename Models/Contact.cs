namespace KutahyaUstunTicaret.Models
{
	public class Contact
	{
        public int ContactId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string ContactType { get; set; } //Telefon ya da mail adresi istenecek
    }
}
