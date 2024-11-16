using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_2.Data;
using Quiz_2.Models;

namespace Quiz_2.Repositories
{
	public class BookRepository(ApplicationDbContext context) : IBookRepository
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<List<Book>> GetBooks()
		{
			return await _context.Books
				.Include(b => b.Authors)
				.Include(b => b.Genres)
				.Select(b => new Book
				{
					Id = b.Id,
					Title = b.Title,
					PublishedYear = b.PublishedYear,
					Authors = b.Authors,
					Genres = b.Genres,
				})
				.ToListAsync();
		}
		public async Task<Book?> GetBookById(int id)
		{
			return await _context.Books
				.Include(b => b.Authors)
				.Include(b => b.Genres)
				.Select(b => new Book
				{
					Id = b.Id,
					Title = b.Title,
					PublishedYear = b.PublishedYear,
					Authors = b.Authors,
					Genres = b.Genres,
				})
				.FirstOrDefaultAsync(b => b.Id == id);
		}
		public async Task<Book?> GetBookByIdLazy(int id)
		{
			return await _context.Books.FindAsync(id);
		}

		public async Task<bool> UpdateBook(int id, BookDto dto)
		{
			var book = await GetBookById(id);

			if (book == null)
				return false;

			book.Title = dto.Title;
			book.PublishedYear = dto.PublishedYear;

			var author = await _context.Authors.FindAsync(dto.AuthorId);

			if (author != null)
				book.Authors.Add(author);

			var genre = await _context.Genre.FindAsync(dto.GenreId);
			if (genre != null)
				book.Genres.Add(genre);
			
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<string> AddBook(BookDto dto)
		{
			var book = new Book
			{
				Title = dto.Title,
				PublishedYear = dto.PublishedYear,
				Authors = new List<Author>(),
				Genres = new List<Genre>(),
			};

			var author = await _context.Authors.FindAsync(dto.AuthorId);

			if (author != null)
				book.Authors.Add(author);

			var genre = await _context.Genre.FindAsync(dto.GenreId);
			if (genre != null)
				book.Genres.Add(genre);

			await _context.Books.AddAsync(book);

			try
			{
				await _context.SaveChangesAsync();
				return $"Add Book {book.Title} Successfully";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

		}

		public async Task<bool> DeleteBook(int id)
		{
			var book = await GetBookByIdLazy(id);

			if (book == null)
				return false;

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
