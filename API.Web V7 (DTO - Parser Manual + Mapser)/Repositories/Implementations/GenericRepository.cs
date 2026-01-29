using System.Data;
using System.Net.Http.Headers;
using API.Context;
using API.Models.Base;
using API.Repositories.Contracts;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;
    private readonly IDbConnection _dbConnection;
    private readonly DbSet<T> _dataset;
    private readonly IEntityAttributesProvider _entityAttributesProvider;

    public GenericRepository(AppDbContext dbContext, IDbConnection dbConnection, IEntityAttributesProvider entityAttributesProvider)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
        _entityAttributesProvider = entityAttributesProvider;
        _dataset = _dbContext.Set<T>();
    }

    public async Task<List<T>> FindAll()
    {
        string tableName = _entityAttributesProvider.GetTableName<T>();
        string columns = _entityAttributesProvider.GetColumns<T>();

        string query = $"SELECT {columns} FROM {tableName}";

        var result = (await _dbConnection.QueryAsync<T>(query)).ToList();
        return result;
    }

    public async Task<T> FindById(long id)
    {
        string tableName = _entityAttributesProvider.GetTableName<T>();
        string columns = _entityAttributesProvider.GetColumns<T>();

        string query = $"SELECT {columns} FROM {tableName} WHERE id = @id";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { id });
        return result!;
    }

    public T Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public async Task Delete(long id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<T> Update(T entity)
    {
        var existingEntity = await _dbContext.Set<T>().FindAsync(entity.Id);

        if (existingEntity != null)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var newValue = property.GetValue(entity);
                property.SetValue(existingEntity, newValue);
            }

            await _dbContext.SaveChangesAsync();
        }

        return existingEntity!;
    }

    public bool Exists(long id)
    {
        return _dataset.Any(entity => entity.Id == id);
    }
}