using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_2.Data;
using Quiz_2.Models;
using Quiz_2.Repositories;

namespace Quiz_2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AuthorsController(IAuthorRepository authorRepo, IJwtTokenService jwtToken) : ControllerBase
	{
		private readonly IAuthorRepository _authorRepo = authorRepo;
		private readonly IJwtTokenService _jwtToken = jwtToken;

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
		{
			return Ok(await _authorRepo.GetAuthors());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Author>> GetAuthor(int id)
		{
			var result = await _authorRepo.GetAuthorById(id);

			if (result != null)
				return Ok(result);
			else
				return NotFound();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromForm] AuthorDto authorDto)
		{
			try
			{
				if(await _authorRepo.UpdateAuthor(id, authorDto))
					return Ok($"Author \'{authorDto.Name}\' updated successfully");
				else
					return NotFound($"Author \'{authorDto.Name}\' not found");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> PostAuthor([FromForm] AuthorDto authorDto)
		{
			if (ModelState.IsValid)
			{
				return Ok(await _authorRepo.RegisterAuthor(authorDto));
			}
			return BadRequest(ModelState);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
		{
			try
			{
				if (await _authorRepo.DeleteAuthor(id))
					return Ok($"Author id {id} deleted successfully");
				else
					return NotFound();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[AllowAnonymous]
		[HttpGet("GenerateToken")]
		public IActionResult GetToken(string email)
		{
			if(_authorRepo.EmailExist(email))
				return Ok(new { token = _jwtToken.GenerateToken(email)});
			else
				return NotFound();
		}
	}
}
