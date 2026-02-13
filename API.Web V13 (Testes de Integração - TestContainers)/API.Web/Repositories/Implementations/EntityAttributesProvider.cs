using System.ComponentModel.DataAnnotations.Schema;
using API.Repositories.Contracts;

namespace API.Repositories.Implementations;

public class EntityAttributesProvider : IEntityAttributesProvider
{
    public string GetColumns<T>()
    {
        var properties = typeof(T).GetProperties();

        var columnNames = properties.Select(property =>
        {
            var columnAttribute = property
                .GetCustomAttributes(typeof(ColumnAttribute), true)
                .FirstOrDefault() as ColumnAttribute;

            if (columnAttribute == null)
            {
                throw new InvalidOperationException(
                    $"The property {property.Name} of entity {typeof(T).Name} does not have a column attribute."
                );
            }

            return $"{columnAttribute.Name} AS {property.Name}";
        });

        return string.Join(", ", columnNames);
    }

    string IEntityAttributesProvider.GetTableName<T>()
    {
        var attributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);

        if (attributes.Length == 0)
            throw new InvalidOperationException($"The entity {typeof(T).Name} does not have a table attribute.");

        var tableAttribute = (TableAttribute)attributes[0];
        return tableAttribute.Name;
    }
}