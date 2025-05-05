using EFCore.Data;
using EFCore.Data.Repositories;
using EFCore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly IMoviesRepository _moviesRepository;

    public MoviesController(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _moviesRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var movie = await _moviesRepository.Get(id);

        return movie == null
            ? NotFound()
            : Ok(movie);
    }

    [HttpGet("until-age/{ageRating}")]
    [ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
    {
        var filteredTitles = await _moviesRepository.All()
            .Where(movie => movie.AgeRating <= ageRating)
            .Select(movie => new MovieTitle { Id = movie.Id, Title = movie.Title })
            .ToListAsync();

        return Ok(filteredTitles);
    }

    [HttpGet("by-year/{year:int}")]
    [ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByYear([FromRoute] int year)
    {
        IEnumerable<MovieTitle> filteredMovies = await _moviesRepository.All()
            .Where(m => m.ReleaseDate.Year == year)
            .Select(m => new MovieTitle
            {
                Id = m.Id,
                Title = m.Title
            })
            .ToListAsync();
        
        return Ok(filteredMovies);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await _moviesRepository.Create(movie);
        
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Movie movie)
    {
        var existingMovie = await _moviesRepository.Update(id, movie);
        
        return Ok(existingMovie);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        await _moviesRepository.Delete(id);
        
        return Ok();
    }
    
    
    
    
    
    
}