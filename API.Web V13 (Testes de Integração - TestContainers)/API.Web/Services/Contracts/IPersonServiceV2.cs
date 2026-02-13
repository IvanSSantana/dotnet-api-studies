using API.Data.DTOs.V2;

namespace API.Services.Contracts.V2;

public interface IPersonServiceV2
{
    PersonDTO Create(PersonDTO person);
    Task<PersonDTO> FindById(long id);
    Task<List<PersonDTO>> FindAll();
    Task<PersonDTO> Update(PersonDTO person);
    Task Delete(long id);

}