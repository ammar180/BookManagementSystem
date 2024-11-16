using System.ComponentModel.DataAnnotations;

namespace Quiz_2.Models
{
	public class Genre
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
	public class GenreDto
	{
		[Required]
		public string Name { get; set; }
    }
}
