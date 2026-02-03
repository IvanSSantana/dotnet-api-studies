using API.Data.DTOs.V1;

namespace API.Services.Contracts.V1;

public interface IPersonService
{
    PersonDTO Create(PersonDTO person);
    Task<PersonDTO> FindById(long id);
    Task<List<PersonDTO>> FindAll();
    Task<PersonDTO> Update(PersonDTO person);
    Task Delete(long id);

}