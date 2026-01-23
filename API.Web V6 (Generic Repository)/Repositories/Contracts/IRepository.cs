using API.Models.Base;

namespace API.Repositories.Contracts;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> FindAll();
    Task<T> FindById(long id);
    T Create(T entity);
    Task<T> Update(T entity);
    Task Delete(long id);
    bool Exists(long id);
}