using API.Models;
using API.Services.Contracts;

namespace API.Services.Implementations;

public class PersonService : IPersonService
{
    public Person FindById(long id)
    {
        Person person = MockPerson(id);

        return person;
    }

    public List<Person> FindAll()
    {
        List<Person> persons = new();
        
        for (long i = 0; i < 5; i++)
        {
            persons.Add(MockPerson(i));
        };

        return persons;
    }

    public Person Create(Person person)
    {
        person.Id = (long) (new Random().NextDouble() * 1_000_000);    

        return person;
    }

    public void Delete(long id)
    {
        // Nothing here
    }

    public Person Update(Person person)
    {   
        return person;
    }

    private Person MockPerson(long id)
    {
        Person person = new()
        {
          Id = (long) (new Random().NextDouble() * 1_000_000),
          FirstName = "Itamar " + id,
          LastName = "Mocks",
          Adress = $"Bread With Butter Avenue 5" + (id % 3 == 0 ? "43" : "76"),
          Gender = (id % 2 == 0) ? "M" : "F"
        };

        return person;
    }
}