using API.Data.Converter.Implementations;
using API.Data.DTOs;
using API.Models;
using FluentAssertions;

namespace API.Tests;

public class PersonConverterTests
{
    private readonly PersonConverter _converter ;

    public PersonConverterTests()
    {
        _converter = new PersonConverter();
    }

    // Objects for testing
    PersonDTO dtoTest = new PersonDTO
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Address = "123 Main St"
    };

    Person personTest = new Person
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Address = "123 Main St"
    };
    
    // Tests
    [Fact]
    public void Parse_ShouldConvertPersonDTOToPerson()
    {
        Person result = _converter.Parse(dtoTest);

        result.Should().NotBeNull();
        Assert.Equal(dtoTest.Id, result.Id);
        Assert.Equal(dtoTest.FirstName, result.FirstName);
        Assert.Equal(dtoTest.LastName, result.LastName);
        Assert.Equal(dtoTest.Address, result.Address);

        result.Should()
            .BeEquivalentTo(personTest);
    } 

    [Fact]
    public void Parse_NullDTOShouldReturnNull()
    {
        PersonDTO dtoNull = null!;

        Person result = _converter.Parse(dtoNull);

        result.Should().BeNull();
    }

    [Fact]
    public void Parse_ShouldConvertPersonToPersonDTO()
    {
        PersonDTO result = _converter.Parse(personTest);

        result.Should().NotBeNull();
        Assert.Equal(dtoTest.Id, result.Id);
        Assert.Equal(dtoTest.FirstName, result.FirstName);
        Assert.Equal(dtoTest.LastName, result.LastName);
        Assert.Equal(dtoTest.Address, result.Address);

        result.Should()
            .BeEquivalentTo(dtoTest);
    }

    [Fact]
    public void Parse_NullPersonShouldReturnNull()
    {
        Person personNull = null!;

        PersonDTO result = _converter.Parse(personNull);
        result.Should().BeNull();
    }

    [Fact]
    public void ParseList_ShouldConvertListOfPersonDTOToListOfPerson()
    {
        List<PersonDTO> dtoList = new() { dtoTest, dtoTest };
        
        List<Person> result = _converter.ParseList(dtoList);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].Should().BeEquivalentTo(personTest);
        result[1].Should().BeEquivalentTo(personTest);
    }

    [Fact]
    public void ParseList_NullPersonListShouldReturnNull()
    {
        List<Person> personList = null!;

        List<PersonDTO> result = _converter.ParseList(personList);
        result.Should().BeNull();
    }

    [Fact]
    public void ParseList_ShouldConvertListOfPersonToListOfPersonDTO()
    {
        List<Person> personList = new() { personTest, personTest };
        
        List<PersonDTO> result = _converter.ParseList(personList);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].Should().BeEquivalentTo(dtoTest);
        result[1].Should().BeEquivalentTo(dtoTest);
    }

    [Fact]
    public void ParseList_NullPersonDTOListShouldReturnNull()
    {
        List<PersonDTO> personDTOList = null!;

        List<Person> result = _converter.ParseList(personDTOList);
        result.Should().BeNull();
    }
}
