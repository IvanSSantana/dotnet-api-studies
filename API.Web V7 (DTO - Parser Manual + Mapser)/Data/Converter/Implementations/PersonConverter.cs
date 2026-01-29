using API.Data.Converter.Contracts;
using API.Models;
using API.Data.DTOs;

namespace API.Data.Converter.Implementations;

public class PersonConverter : IParser<Person, PersonDTO>, IParser<PersonDTO, Person>
{
    public PersonDTO Parse(Person origin)
    {
        if (origin == null) return null!;

        return new PersonDTO
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address =  origin.Address,
            Gender = origin.Gender
        };
    }

    public Person Parse(PersonDTO origin)
    {
        if (origin == null) return null!;

        return new Person
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address =  origin.Address,
            Gender = origin.Gender
        };
    }

    public List<PersonDTO> ParseList(List<Person> origin)
    {
        if (origin == null) return null!;
        return origin.Select(item => Parse(item)).ToList();
    }

    public List<Person> ParseList(List<PersonDTO> origin)
    {
        if (origin == null) return null!;
        return origin.Select(item => Parse(item)).ToList();
    }
}