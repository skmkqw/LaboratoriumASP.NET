using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class MoviesDbContext : DbContext
{
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<MovieCastEntity> MovieCasts { get; set; }
    
    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options)
    {
    }
}