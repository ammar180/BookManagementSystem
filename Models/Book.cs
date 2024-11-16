using System.ComponentModel.DataAnnotations;

namespace Quiz_2.Models
{
	public class Book
	{
		[Key]
        public int Id { get; set; }
		[Required]
		public string Title { get; set; }
        public DateOnly PublishedYear { get; set; }
        public ICollection<Author> Authors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }	
    }
    public class BookDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
        public DateOnly PublishedYear { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
