using API.Models;

namespace API.Services.Contracts;

public interface IPersonService
{
    Person Create(Person person);
    Person FindById(long id);
    List<Person> FindAll();
    Person Update(Person person);
    void Delete(long id);

}