using EFCore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
    public async Task<IList<Movie>> GetAll()
    {
        
        return [new Movie { Id = 1, Title = "The Matrix"}];
    }
}