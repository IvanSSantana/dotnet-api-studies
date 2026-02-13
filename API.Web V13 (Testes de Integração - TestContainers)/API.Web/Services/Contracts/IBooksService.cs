using API.Data.DTOs.V1;

namespace API.Services.Contracts.V1;

public interface IBooksService
{
    BookDTO Create(BookDTO book);
    Task<BookDTO> FindById(long id);
    Task<List<BookDTO>> FindAll();
    Task<BookDTO> Update(BookDTO book);
    Task Delete(long id);

}