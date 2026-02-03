using Microsoft.AspNetCore.Mvc;
using API.Services.Contracts.V2;
using API.Data.DTOs.V2;

namespace API.Controllers.V2;

[ApiController]
[Route("api/[controller]/v2")]
public class PersonController : ControllerBase
{
    private readonly IPersonServiceV2 _personService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonServiceV2 personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonDTO person)
    {
        PersonDTO createdPerson = _personService.Create(person);

        if (person == null)
        {
            _logger.LogError("Failed to create person with provided data");
            return NotFound();
        } 
        
        _logger.LogInformation("Created person with Id {id}", createdPerson.Id);
        return Created("", createdPerson);
    }
}