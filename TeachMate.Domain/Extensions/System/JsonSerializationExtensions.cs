using System.Text.Json;

namespace TeachMate.Domain;
public static class JsonSerializationExtensions
{
    public static string ToJson(this object obj)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(obj, serializeOptions);
    }
}
