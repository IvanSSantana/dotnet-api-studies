namespace API.Models;

public class Person
{
    public long Id { get; set; }
    public string  FirstName { get; set; } = default!;
    public string  LastName { get; set; } = default!;
    public string  Adress { get; set; } = default!;
    public string  Gender { get; set; } = default!;
    
    
}