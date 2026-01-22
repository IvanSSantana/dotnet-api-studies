using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services.Contracts;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Getting all persons");
        return Ok(await _personService.FindAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        Person person = await _personService.FindById(id);

        if (person == null)
        {
            _logger.LogWarning("Person with Id {id} not found", id);
            return NotFound();
        } 

        _logger.LogInformation("Getting person with Id {id}", id);
        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        Person createdPerson = _personService.Create(person);

        if (person == null)
        {
            _logger.LogError("Failed to create person with provided data");
            return NotFound();
        } 

        _logger.LogInformation("Created person with Id {id}", createdPerson.Id);
        return Created("", createdPerson);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Person person)
    {
        Person updatedPerson = await _personService.Update(person);

        if (person == null)
        {
            _logger.LogError("Failed to update person with Id {id}", person!.Id);
            return NotFound();
        } 

        _logger.LogDebug("Updated person with Id {id}", updatedPerson.Id);
        return Ok(updatedPerson);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _personService.Delete(id);

        _logger.LogInformation("Deleted person with Id {id}", id);
        return NoContent();
    }
}