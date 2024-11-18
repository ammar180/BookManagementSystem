using Quiz_2.Models;

namespace Quiz_2.Data
{
    public class AuthorWithBooksDto
    {
        public AuthorDto authorDto { get; set; }
        public List<BookDto>? authorBooks { get; set; }
    }
}
