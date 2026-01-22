using API.Models;

namespace API.Repositories.Contracts;

public interface IPersonRepository
{
    Person Create(Person person);
    Task<Person> FindById(long id);
    Task<List<Person>> FindAll();
    Task<Person> Update(Person person);
    Task Delete(long id);

}