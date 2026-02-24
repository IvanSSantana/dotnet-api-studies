using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services.Contracts.V1;
using Mapster;
using API.Data.DTOs.V1;

namespace API.Controllers.V1;

[ApiController]
[Route("api/[controller]/v1")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBooksService booksService, ILogger<BooksController> logger)
    {
        _booksService = booksService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Getting all books");
        return Ok(await _booksService.FindAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id)
    {
        Book book = (await _booksService.FindById(id)).Adapt<Book>();

        if (book == null)
        {
            _logger.LogWarning("Book with Id {id} not found", id);
            return NotFound();
        } 

        _logger.LogInformation("Getting book with Id {id}", id);
        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Post([FromBody] BookDTO book)
    {
        BookDTO createdBook = _booksService.Create(book);

        if (book == null)
        {
            _logger.LogError("Failed to create book with provided data");
            return NotFound();
        } 

        _logger.LogInformation("Created book with Id {id}", createdBook.Id);
        return Created("", createdBook);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] BookDTO book)
    {
        BookDTO updatedBook = await _booksService.Update(book);
        
        if (updatedBook == null)
        {
            _logger.LogWarning("Failed to update book with Id {id}", book.Id);
            return NotFound();
        }

        _logger.LogInformation("Updated book with Id {id}", updatedBook.Id);
        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await _booksService.Delete(id);
        _logger.LogInformation("Deleted book with Id {id}", id);
        return NoContent();
    }
}