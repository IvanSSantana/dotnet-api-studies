using API.Data.DTOs;

namespace API.Services.Contracts;

public interface IPersonService
{
    PersonDTO Create(PersonDTO person);
    Task<PersonDTO> FindById(long id);
    Task<List<PersonDTO>> FindAll();
    Task<PersonDTO> Update(PersonDTO person);
    Task Delete(long id);

}