using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("person")]
public class Person
{     
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("first_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string  FirstName { get; set; } = default!;

    [Required]
    [Column("last_name")]
    [MaxLength(80)]
    public string  LastName { get; set; } = default!;

    [Required]
    [Column("address", TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string  Address { get; set; } = default!;

    [Required]
    [Column("gender", TypeName = "varchar(1)")]
    [MaxLength(1)]
    public string  Gender { get; set; } = default!;
    
    
}