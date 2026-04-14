using System.Text.Json;
using System.Text.Json.Serialization;

namespace PR_5_DataLibrary.Infrastructure;

public class JsonDataSerializer : IDataSerializer
{
    // Shared options for all serialization calls
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public JsonDataSerializer()
    {
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            // Pretty print JSON files
            WriteIndented = true,

            // Store enums as strings (e.g. "Ghost" instead of 1)
            Converters = { new JsonStringEnumConverter() }
        };
    }
    
    // Object -> JSON string
    public string Serialize<T>(T data)
    {
        return JsonSerializer.Serialize(data, _jsonSerializerOptions);
    }
    
    // JSON string -> object (null if invalid)
    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
    }
}