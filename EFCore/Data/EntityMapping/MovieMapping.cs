using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Movies")
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(m => m.ReleaseDate)
            .HasColumnType("date");
        
        builder
            .Property(m => m.Synopsis)
            .HasColumnType("varchar(max)");

        builder
            .Property(movie => movie.AgeRating)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasPrincipalKey(g => g.Id)
            .HasForeignKey(m => m.MainGenreId);

        builder.OwnsOne(movie => movie.Director)
            .ToTable("MovieDirectors");

        builder.OwnsMany(movie => movie.Actors)
            .ToTable("Movie_Actors");
        
        // Seed - data that needs to be created always
        builder.HasData(new Movie
        {
            Id = 1,
            Title = "The Matrix",
            ReleaseDate = new DateTime(1999, 12, 15),
            Synopsis = "Cool movie",
            MainGenreId = 1,
            AgeRating = AgeRating.Adolescent
        });

        builder.OwnsOne(movie => movie.Director)
            .HasData(new
            {
                FirstName = "David",
                LastName = "Fincher",
                MovieId = 1
            });
        
        builder.OwnsMany(movie => movie.Actors)
            .HasData(new
            {
                FirstName = "Kianu",
                LastName = "Reeves",
                Id = 1,
                MovieId = 1
            },
            new {
                FirstName = "John",
                LastName = "Smith",
                Id = 2,
                MovieId = 1
            });
    }
}