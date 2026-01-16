using System.Data;
using API.Context;
using API.Models;
using API.Services.Contracts;
using Dapper;

namespace API.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly IDbConnection _dbConnection;
    private readonly AppDbContext _dbContext;

    public PersonService(IDbConnection dbConnection, AppDbContext dbContext)
    {
        _dbConnection = dbConnection;
        _dbContext = dbContext;
    }

    public async Task<Person> FindById(long id)
    {
        string query = """
        SELECT id AS Id,
            first_name AS FirstName,
            last_name AS LastName,
            address AS Address,
            gender AS Gender
        FROM person
        WHERE Id = @Id;
        """; // Proteção a SQL Injection
        Person person = await _dbConnection.QueryFirstOrDefaultAsync<Person>(query, new { Id = id }) ?? null!;

        return person;
    }

    public async Task<List<Person>> FindAll()
    {
        string query = """
        SELECT id AS Id,
            first_name AS FirstName,
            last_name AS LastName,
            address AS Address,
            gender AS Gender
        FROM person
        ORDER BY id;
        """; 

        List<Person> persons = (await _dbConnection.QueryAsync<Person>(query)).ToList();

        return persons;
    }

    public Person Create(Person person)
    {
       _dbContext.Persons.Add(person);
       _dbContext.SaveChanges();

        return person;
    }

    public async Task<Person> Update(Person person)
    {  
        Person existingPerson = await _dbContext.Persons.FindAsync(person.Id) ?? null!;

        if (existingPerson is null) return null!;

        _dbContext.Entry(existingPerson).CurrentValues.SetValues(person); // Permite atualização parcial e não quebra com overposting
        _dbContext.SaveChanges();

        return existingPerson;
    }

    public async Task Delete(long id)
    {
        Person dbPerson = await _dbContext.Persons.FindAsync(id) ?? null!;

        if (dbPerson is null) return;
        
        _dbContext.Persons.Remove(dbPerson);
        _dbContext.SaveChanges();
    }
}