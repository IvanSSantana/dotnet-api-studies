using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Base;

namespace API.Models;

[Table("books")]
public class Book : BaseEntity
{     
    [Required]
    [Column("title", TypeName = "varchar(200)")]
    [MaxLength(200)]
    public string  Title { get; set; } = default!;

    [Required]
    [Column("author", TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string  Author { get; set; } = default!;

    [Required]
    [Column("price", TypeName = "decimal(18,2)")]
    public decimal  Price { get; set; }

    [Required]
    [Column("launch_date", TypeName = "datetime2(6)")]
    public DateTime  LaunchDate { get; set; }
}