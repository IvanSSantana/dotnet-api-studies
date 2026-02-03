using API.Data.DTOs;
using API.Models;
using API.Repositories.Contracts;
using API.Services.Contracts;
using Mapster;

namespace API.Services.Implementations;

public class BooksService : IBooksService
{
    private readonly IRepository<Book> _booksRepository;

    public BooksService(IRepository<Book> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public BookDTO Create(BookDTO book)
    {
        Book bookEntity = book.Adapt<Book>();
        return _booksRepository.Create(bookEntity).Adapt<BookDTO>();
    }

    public async Task<BookDTO> FindById(long id)
    {
        Book book = await _booksRepository.FindById(id);
        return book.Adapt<BookDTO>();
    }

    public async Task<List<BookDTO>> FindAll()
    {
        List<Book> books = await _booksRepository.FindAll();
        return books.Adapt<List<BookDTO>>();
    }

    public async Task<BookDTO> Update(BookDTO book)
    {
        Book bookEntity = book.Adapt<Book>();
        return (await _booksRepository.Update(bookEntity)).Adapt<BookDTO>();
    }

    public Task Delete(long id)
    {
        return _booksRepository.Delete(id);
    }
}