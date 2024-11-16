using Microsoft.EntityFrameworkCore;
using Quiz_2.Models;

namespace Quiz_2.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Author> Authors { get; set; } = default!;
	    public DbSet<Genre> Genre { get; set; } = default!;
	    public DbSet<Book> Books { get; set; } = default!;
	}
}
