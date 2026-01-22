using API.Models;

namespace API.Repositories.Contracts;

public interface IBooksRepository
{
    Book Create(Book book);
    Task<Book> FindById(long id);
    Task<List<Book>> FindAll();
    Task<Book> Update(Book book);
    Task Delete(long id);
}