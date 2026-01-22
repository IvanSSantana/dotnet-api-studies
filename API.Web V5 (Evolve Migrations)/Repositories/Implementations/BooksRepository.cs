using API.Repositories.Contracts;
using API.Models;
using API.Context;
using Dapper;
using System.Data;

namespace API.Repositories.Implementations;

public class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IDbConnection _dbConnection;

    public BooksRepository(AppDbContext dbContext, IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    public Book Create(Book book)
    {
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

        return book;
    }

    public async Task Delete(long id)
    {
        Book? book = await FindById(id);
        if (book != null)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Book>> FindAll()
    {
        string query = """
        SELECT id AS Id,
            title AS Title,
            author AS Author,
            price AS Price,
            launch_date AS LaunchDate
        FROM books
        ORDER BY id;
        """; 

        List<Book> books = (await _dbConnection.QueryAsync<Book>(query)).ToList();

        return books;
    }

    public async Task<Book> FindById(long id)
    {
        string query = """
        SELECT id AS Id,
            title AS Title,
            author AS Author,
            price AS Price,
            launch_date AS LaunchDate
        FROM books
        WHERE Id = @Id;
        """; 
        Book book = await _dbConnection.QueryFirstOrDefaultAsync<Book>(query, new { Id = id }) ?? null!;
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        var existingBook = await _dbContext.Books.FindAsync(book.Id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Price = book.Price;
            existingBook.LaunchDate = book.LaunchDate;

            await _dbContext.SaveChangesAsync();
        }
        return existingBook!;
    }
}