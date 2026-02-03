using System.Text.Json.Serialization;

namespace API.Data.DTOs.V2;

public class PersonDTO
{   
    // [JsonPropertyOrder(1)] // Não é possível trocar a ordem de um atributo somente, mas deve ser todos
    public long Id { get; set; }

    // [JsonPropertyName("first_name")]
    // [JsonPropertyOrder(2)]
    public string FirstName { get; set; } = string.Empty;

    // [JsonPropertyName("last_name")]
    // [JsonPropertyOrder(3)]
    public string LastName { get; set; } = string.Empty;

    // [JsonPropertyOrder(5)]
    public string Address { get; set; } = string.Empty;

    // [JsonPropertyOrder(4)
    [JsonConverter(typeof(JsonSerializers.GenderSerializer))]
    public string Gender { get; set; } = string.Empty;

    // [JsonPropertyName("birth_day")]
    // [JsonPropertyOrder(6)]
    [JsonConverter(typeof(JsonSerializers.DateSerializer))]
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? BirthDay { get; set; }

    // [JsonIgnore]
    // public DateTime CreatedAt => DateTime.UtcNow;
}