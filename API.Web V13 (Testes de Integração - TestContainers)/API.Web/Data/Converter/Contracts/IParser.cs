namespace API.Data.Converter.Contracts;

public interface IParser<TOrigin, TDestination>
{
    TDestination Parse(TOrigin origin);
    List<TDestination> ParseList(List<TOrigin> origin);
}