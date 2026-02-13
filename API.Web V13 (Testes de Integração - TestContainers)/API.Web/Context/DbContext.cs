using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Person> Persons => Set<Person>();   
    public DbSet<Book> Books => Set<Book>();
}