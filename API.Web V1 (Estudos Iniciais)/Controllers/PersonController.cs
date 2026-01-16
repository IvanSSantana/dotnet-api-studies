using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services.Contracts;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_personService.FindAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        Person person = _personService.FindById(id);

        if (person == null) return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        Person createdPerson = _personService.Create(person);

        if (person == null) return NotFound();

        return Created("Person register occurred successful", createdPerson);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Person person)
    {
        Person updatedPerson = _personService.Update(person);

        if (person == null) return NotFound();

        return Ok(updatedPerson);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _personService.Delete(id);

        return NoContent();
    }
}