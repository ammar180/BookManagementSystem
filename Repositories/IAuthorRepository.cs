using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_2.Data;
using Quiz_2.Models;

namespace Quiz_2.Repositories
{
	public interface IAuthorRepository
	{
		Task<List<Author>> GetAuthors();
		Task<Author?> GetAuthorById(int id);
		Task<bool> UpdateAuthor(int id, AuthorDto author);
		Task<string> RegisterAuthor(AuthorDto author, List<BookDto>? booksDtos);
		Task<bool> DeleteAuthor(int id);
		bool EmailExist(string email);
	}
}
