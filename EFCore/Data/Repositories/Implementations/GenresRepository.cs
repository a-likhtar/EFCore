using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data.Repositories.Implementations;

public class GenresRepository : IGenresRepository
{
    private readonly MoviesContext _context;

    public GenresRepository(MoviesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> Get(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<Genre?> Create(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();

        return genre;
    }

    public async Task<Genre?> Update(int id, Genre genre)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return null;

        existingGenre.Name = genre.Name;

        await _context.SaveChangesAsync();

        return genre;
    }

    public async Task<bool> Delete(int id)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return false;

        _context.Genres.Remove(existingGenre);

        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Genre> All() => _context.Genres.AsQueryable();
}