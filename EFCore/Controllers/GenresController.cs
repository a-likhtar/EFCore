using EFCore.Data;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : Controller
{
    private readonly MoviesContext _moviesContext;

    public GenresController(MoviesContext moviesContext)
    {
        _moviesContext = moviesContext;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await _moviesContext.Genres.SingleOrDefaultAsync(g => g.Id == id);

        return genre == null
            ? NotFound()
            : Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre)
    {
        await _moviesContext.Genres.AddAsync(genre);
        await _moviesContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
    }
}