using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data.Repositories.Implementations;

public class MoviesRepository : IMoviesRepository
{
    private readonly MoviesContext _context;

    public MoviesRepository(MoviesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie?> Get(int id)
    {
        return await _context.Movies.FindAsync(id);
    }

    public async Task<Movie?> Create(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie?> Update(int id, Movie movie)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie is null)
            return null;

        existingMovie.Title = movie.Title;
        existingMovie.Actors = movie.Actors;
        existingMovie.AgeRating = movie.AgeRating;
        existingMovie.Director = movie.Director;
        existingMovie.InternetRating = movie.InternetRating;
        existingMovie.Genre = movie.Genre;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.Synopsis = movie.Synopsis;

        await _context.SaveChangesAsync();

        return existingMovie;
    }

    public async Task<bool> Delete(int id)
    {
        var existingMovie = await _context.Movies.FindAsync(id);

        if (existingMovie is null)
            return false;

        _context.Movies.Remove(existingMovie);

        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Movie> All() => _context.Movies.AsQueryable();
}