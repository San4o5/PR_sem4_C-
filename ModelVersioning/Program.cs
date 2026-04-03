using System.Text.Json;

namespace ModelVersioning;

class Program
{
    static void Main(string[] args)
    {
        // Старий JSON — поля Level не існує
        string oldJson = """
                         {
                             "Name": "Jack"
                         }
                         """;
        var jsonOptions = new JsonSerializerOptions //Клод сказав так писати більш правильно
        {
            WriteIndented = true
        };

        Player player = JsonSerializer.Deserialize<Player>(oldJson, jsonOptions);
        Console.WriteLine($"Download old JSON in new model version: \n{player.Name}");
        Console.WriteLine($"Level: {player.Level}");// поверне 1, а не впаде
        
        // Серіалізуємо вже з новим полем
        string newJson = JsonSerializer.Serialize<Player>(player, jsonOptions);
        Console.WriteLine($"Serialized new JSON: \n{newJson}");
    }
}