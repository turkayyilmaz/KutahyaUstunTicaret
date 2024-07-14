namespace KutahyaUstunTicaret.Models
{
	public class MessageViewModel
	{
		public int MessageId { get; set; }
		public string FullName { get; set; }
		public string EMail { get; set; }
		public string Title { get; set; }
		public string MessageContent { get; set; }
		public bool IsRead { get; set; }

	}
}
