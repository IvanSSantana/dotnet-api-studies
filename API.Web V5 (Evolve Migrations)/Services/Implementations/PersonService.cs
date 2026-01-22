using API.Models;
using API.Repositories.Contracts;
using API.Services.Contracts;

namespace API.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> FindById(long id)
    {

        return await _personRepository.FindById(id);
    }

    public async Task<List<Person>> FindAll()
    {
        return await _personRepository.FindAll();
    }

    public Person Create(Person person)
    {
        return _personRepository.Create(person);
    }

    public async Task<Person> Update(Person person)
    {  
        return await _personRepository.Update(person);
    }

    public async Task Delete(long id)
    {
        await _personRepository.Delete(id);
    }
}