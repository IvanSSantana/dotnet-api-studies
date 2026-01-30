namespace API.Repositories.Contracts;

public interface IEntityAttributesProvider
{
    string GetTableName<T>();
    string GetColumns<T>();
}