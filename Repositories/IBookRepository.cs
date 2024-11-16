using Quiz_2.Models;

namespace Quiz_2.Repositories
{
    public interface IBookRepository
    {
		Task<List<Book>> GetBooks();
		Task<Book?> GetBookById(int id);
		Task<bool> UpdateBook(int id, BookDto author);
		Task<string> AddBook(BookDto author);
		Task<bool> DeleteBook(int id);
	}
}