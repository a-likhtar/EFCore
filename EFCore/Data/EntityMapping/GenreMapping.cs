using EFCore.Data.ValueGenerators;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(genre => genre.CreatedDate)
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder.HasData(new Genre()
        {
            Id = 1,
            Name = "Fantastic"
        });
    }
}