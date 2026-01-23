using API.Models;

namespace API.Services.Contracts;

public interface IBooksService
{
    Book Create(Book book);
    Task<Book> FindById(long id);
    Task<List<Book>> FindAll();
    Task<Book> Update(Book book);
    Task Delete(long id);

}