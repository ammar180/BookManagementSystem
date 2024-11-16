using Microsoft.AspNetCore.Mvc;
using Quiz_2.Models;
using Quiz_2.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Quiz_2.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BooksController(IBookRepository bookRepo) : ControllerBase
	{
		private readonly IBookRepository _bookRepo = bookRepo;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			return Ok(await _bookRepo.GetBooks());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook(int id)
		{
			var result = await _bookRepo.GetBookById(id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutBook(int id, [FromForm] BookDto bookDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (await _bookRepo.UpdateBook(id, bookDto))
						return Ok($"Book \'{bookDto.Title}\' updated successfully");
					else
						return NotFound($"Book \'{bookDto.Title}\' not found");
				}
				else
					throw new ValidationException("Data doesn't valid");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostBook([FromForm] BookDto bookDto)
		{
			if (ModelState.IsValid)
				return Ok(await _bookRepo.AddBook(bookDto));
			else
				return BadRequest(ModelState);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			try
			{
				if (await _bookRepo.DeleteBook(id))
					return Ok($"Book id {id} deleted successfully");
				else
					return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
