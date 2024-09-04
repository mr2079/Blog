using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Comment.Api.Persistence.Converters;

public class StringToObjectIdConverter : JsonConverter
{
    public override void WriteJson(
        JsonWriter writer,
        object? value,
        JsonSerializer serializer)
    {
        serializer.Serialize(writer, value?.ToString());
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        return ObjectId.Parse(token.ToObject<string>());
    }

    public override bool CanConvert(Type objectType)
        => objectType == typeof(ObjectId);
}