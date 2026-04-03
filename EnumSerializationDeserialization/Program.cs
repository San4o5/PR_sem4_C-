using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumSerializationDeserialization;

class Program
{
    static void Main(string[] args)
    {
        Order order = new Order { Id = 1, Status = OrderStatus.Pending };
        
        // Без конвертера — enum серіалізується як число
        JsonSerializerOptions jsonOptions = new() { WriteIndented = true };
        string jsonDefault = JsonSerializer.Serialize(order, jsonOptions);
        Console.WriteLine(jsonDefault);
        
        // З JsonStringEnumConverter — enum серіалізується як текст
        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        string json = JsonSerializer.Serialize(order, options);
        Console.WriteLine(json);
        
        // Десеріалізація назад
        Order? loaded =  JsonSerializer.Deserialize<Order>(json, options);
        Console.WriteLine("After deserialization");
        Console.WriteLine($"Order Id: {order.Id}\nStatus: {order.Status}");
    }
}