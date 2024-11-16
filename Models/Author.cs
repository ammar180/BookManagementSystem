using System.ComponentModel.DataAnnotations;

namespace Quiz_2.Models
{
	public class Author
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Phone()]
        public string PhoneNumber { get; set; }
		[EmailAddress]
        public string EmailAddress { get; set; }
        public ICollection<Book> Books { get; set; }
    }
	public class AuthorDto
	{
		[Required]
		public string Name { get; set; }
		[Phone()]
        public string PhoneNumber { get; set; }
		[EmailAddress]
        public string EmailAddress { get; set; }
    }
}
