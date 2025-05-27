using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private readonly string _format = "HH:mm";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}