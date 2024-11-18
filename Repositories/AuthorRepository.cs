using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_2.Data;
using Quiz_2.Models;

namespace Quiz_2.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly ApplicationDbContext _context;

		public AuthorRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<Author>> GetAuthors()
		{
			return await _context.Authors
				.Include(a => a.Books)
				.Select(a => new Author
				{
					Id = a.Id,
					Name = a.Name,
					EmailAddress = a.EmailAddress,
					PhoneNumber = a.PhoneNumber,
					Books = a.Books,
				})
				.ToListAsync();
		}
		public async Task<Author?> GetAuthorById(int id)
		{
			return await _context.Authors
				.Include(a => a.Books)
				.Select(a => new Author
				{
					Id = a.Id,
					Name = a.Name,
					EmailAddress = a.EmailAddress,
					PhoneNumber = a.PhoneNumber,
					Books = a.Books,
				})
				.FirstOrDefaultAsync(a => a.Id == id);
		}
		public async Task<Author?> GetAuthorByIdLazt(int id)
		{
			return await _context.Authors.FindAsync(id);
		}

		public async Task<bool> UpdateAuthor(int id, AuthorDto dto)
		{
			var author = await GetAuthorByIdLazt(id);

			if (author == null)
				return false;

			author.Name = dto.Name;
			author.PhoneNumber = dto.PhoneNumber;
			author.EmailAddress = dto.EmailAddress;

			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<string> RegisterAuthor(AuthorDto dto, List<BookDto>? booksDtos)
		{
			var author = new Author
			{
				Name = dto.Name,
				PhoneNumber = dto.PhoneNumber,
				EmailAddress = dto.EmailAddress,
			};
			if (booksDtos != null) {
				author.Books = booksDtos.Select(b => new Book
				{
					Title = b.Title,
					PublishedYear = b.PublishedYear,
				}).ToList();
			}
			await _context.Authors.AddAsync(author);
			try
			{
				await _context.SaveChangesAsync();
				return $"Add Author {author.Name} Successfully";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

		}

		public async Task<bool> DeleteAuthor(int id)
		{
			var author = await GetAuthorByIdLazt(id);

			if (author == null)
				return false;

			_context.Authors.Remove(author);
			await _context.SaveChangesAsync();

			return true;
		}

		public bool EmailExist(string email)
		{
			return _context.Authors.Any(u => u.EmailAddress == email);
		}
	}
}
