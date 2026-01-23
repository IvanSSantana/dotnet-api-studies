using API.Models;
using API.Repositories.Contracts;
using API.Services.Contracts;

namespace API.Services.Implementations;

public class BooksService : IBooksService
{
    private readonly IRepository<Book> _booksRepository;

    public BooksService(IRepository<Book> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public Book Create(Book book)
    {
        return _booksRepository.Create(book);
    }

    public Task<Book> FindById(long id)
    {
        return _booksRepository.FindById(id);
    }

    public Task<List<Book>> FindAll()
    {
        return _booksRepository.FindAll();
    }

    public Task<Book> Update(Book book)
    {
        return _booksRepository.Update(book);
    }

    public Task Delete(long id)
    {
        return _booksRepository.Delete(id);
    }
}