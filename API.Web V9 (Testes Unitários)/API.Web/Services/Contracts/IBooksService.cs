using API.Data.DTOs;

namespace API.Services.Contracts;

public interface IBooksService
{
    BookDTO Create(BookDTO book);
    Task<BookDTO> FindById(long id);
    Task<List<BookDTO>> FindAll();
    Task<BookDTO> Update(BookDTO book);
    Task Delete(long id);

}