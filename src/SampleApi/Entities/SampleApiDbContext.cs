using Microsoft.EntityFrameworkCore;

namespace SampleApi.Entities;

public class SampleApiDbContext : DbContext
{
	public SampleApiDbContext(DbContextOptions<SampleApiDbContext> options) : base(options)
	{

	}
	public DbSet<Product> Products { get; set; }

}