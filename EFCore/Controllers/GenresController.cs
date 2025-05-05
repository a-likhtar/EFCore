using EFCore.Data;
using EFCore.Data.Repositories;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : Controller
{
    private readonly IGenresRepository _genresRepository;

    public GenresController(IGenresRepository genresRepository)
    {
        _genresRepository = genresRepository;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await _genresRepository.Get(id);

        return genre == null
            ? NotFound()
            : Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre)
    {
        await _genresRepository.Create(genre);

        return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
    }
}