using Microsoft.AspNetCore.Mvc;
using API.Services.Contracts.V1;
using API.Data.DTOs.V1;

namespace API.Controllers.V1;

[ApiController]
[Route("api/[controller]/v1")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Getting all persons");
        return Ok(await _personService.FindAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> Get(long id)
    {
        PersonDTO person = await _personService.FindById(id);

        if (person == null)
        {
            _logger.LogWarning("Person with Id {id} not found", id);
            return NotFound();
        } 

        _logger.LogInformation("Getting person with Id {id}", id);
        return Ok(person);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Post([FromBody] PersonDTO person)
    {
        PersonDTO createdPerson = _personService.Create(person);

        if (person == null)
        {
            _logger.LogError("Failed to create person with provided data");
            return NotFound();
        } 

        _logger.LogInformation("Created person with Id {id}", createdPerson.Id);

        // Response.Headers.Add("X-API-Deprecated", "true");
        Response.Headers["X-API-Deprecated"] = "true";
        Response.Headers["X-API-Deprecated-Date"] = "2026-12-31";
        return Created("", createdPerson);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] PersonDTO person)
    {
        PersonDTO updatedPerson = await _personService.Update(person);

        if (person == null)
        {
            _logger.LogError("Failed to update person with Id {id}", person!.Id);
            return NotFound();
        } 

        _logger.LogDebug("Updated person with Id {id}", updatedPerson.Id);
        return Ok(updatedPerson);
    }

    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)    
    {
        PersonDTO entity = await _personService.FindById(id);

        if (entity == null)
        {
            _logger.LogWarning("Person with Id {id} not found for deletion", id);
            return NotFound();
        }
         
        await _personService.Delete(id);

        _logger.LogInformation("Deleted person with Id {id}", id);
        return NoContent();
    }
}