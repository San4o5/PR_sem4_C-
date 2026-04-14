namespace PR_5_DataLibrary;

// Converts objects to/from JSON string
public interface IDataSerializer
{
    string Serialize<T>(T data);
    T? Deserialize<T>(string json);
}