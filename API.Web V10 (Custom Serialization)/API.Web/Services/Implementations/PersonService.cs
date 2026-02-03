using API.Data.Converter.Implementations.V1;
using API.Data.DTOs.V1;
using API.Models;
using API.Repositories.Contracts;
using API.Services.Contracts.V1;

namespace API.Services.Implementations.V1;

public class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;
    private readonly PersonConverter _converter;

    public PersonService(IRepository<Person> personRepository)
    {
        _personRepository = personRepository;
        _converter = new PersonConverter();
    }

    public async Task<PersonDTO> FindById(long id)
    {
        return _converter.Parse(await _personRepository.FindById(id));
    }

    public async Task<List<PersonDTO>> FindAll()
    {
        return _converter.ParseList(await _personRepository.FindAll());
    }

    public PersonDTO Create(PersonDTO person)
    {
        Person personEntity = _converter.Parse(person);
        var createdPerson = _personRepository.Create(personEntity);
        return _converter.Parse(createdPerson);
    }

    public async Task<PersonDTO> Update(PersonDTO person)
    {  
        Person personEntity = _converter.Parse(person);
        var updatedPerson = await _personRepository.Update(personEntity);
        return _converter.Parse(updatedPerson);
    }

    public async Task Delete(long id)
    {
        await _personRepository.Delete(id);
    }
}