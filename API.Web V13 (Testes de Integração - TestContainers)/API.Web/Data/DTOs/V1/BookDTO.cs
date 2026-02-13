namespace API.Data.DTOs.V1;

public class BookDTO
{     
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime LaunchDate { get; set; }
}