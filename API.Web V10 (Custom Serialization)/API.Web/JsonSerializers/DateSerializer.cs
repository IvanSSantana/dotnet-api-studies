using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.JsonSerializers;

public class DateSerializer : JsonConverter<DateTime?>
{
    private readonly string _dateFormat = "dd/MM/yyyy";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParseExact(reader.GetString(), _dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString(_dateFormat));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}