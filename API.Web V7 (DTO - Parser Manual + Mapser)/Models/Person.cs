using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Base;

namespace API.Models;

[Table("person")]
public class Person : BaseEntity
{     
    [Required]
    [Column("first_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string  FirstName { get; set; } = string.Empty;

    [Required]
    [Column("last_name")]
    [MaxLength(80)]
    public string  LastName { get; set; } = string.Empty;

    [Required]
    [Column("address", TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string  Address { get; set; } = string.Empty;

    [Required]
    [Column("gender", TypeName = "varchar(1)")]
    [MaxLength(1)]
    public string  Gender { get; set; } = string.Empty;
}