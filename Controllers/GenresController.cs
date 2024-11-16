using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_2.Data;
using Quiz_2.Models;

namespace Quiz_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

		[HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
        {
            return await _context.Genre.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genre.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDto dto)
        {
            var genre = await _context.Genre.FindAsync(id);

            if (genre == null)
                return NotFound();
            else
            {
                genre.Name = dto.Name;
                await _context.SaveChangesAsync();
            }
            return Ok("Updated Successfully!");
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(GenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };
            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            return Ok($"Genre Name \'{dto.Name}\' created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
