using MyDictionary.Domain.Common;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyDictionary.Api.Extensions;

public class OptionalJsonConverter<T> : JsonConverter<Optional<T>>
{
    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return new Optional<T>(value: default);

        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        return new Optional<T>(value);
    }

    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
            writer.WriteNullValue();
        else
            JsonSerializer.Serialize(writer, value.Value, options);
    }
}