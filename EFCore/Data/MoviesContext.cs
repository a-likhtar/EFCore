using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Movies;User Id=SA;Password=Sbopbxbr2146150!;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }
}