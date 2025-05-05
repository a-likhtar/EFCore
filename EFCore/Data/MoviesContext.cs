using EFCore.Data.EntityMapping;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Movie> Movies => Set<Movie>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new GenreMapping());
    }
}