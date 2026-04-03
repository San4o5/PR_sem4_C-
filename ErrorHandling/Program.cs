using System.Text.Json;

namespace ErrorHandling;

class Program
{
    static Player LoadFromFile(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            Player? player = JsonSerializer.Deserialize<Player>(json);
            if (player == null)
            {
                throw new JsonException("Deserialize failed!");
            }

            return player;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {path}. Creating a new player!");
            return new Player { Name = "Unknown", Level = 1 };
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error reading json: {ex.Message}");
            Console.WriteLine($"JSON damaged. Creating a new player!");
            return new Player { Name = "Unknown", Level = 1 };
        }
    }
    
    static void Main(string[] args)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        // 1. Коректний JSON
        File.WriteAllText("player_good.json", """{"Name":"Jack","Level":5}""");
        Player good = LoadFromFile("player_good.json");
        Console.WriteLine($"[OK] Name: {good.Name}, Level: {good.Level}");
        
        // 2. Пошкоджений JSON
        File.WriteAllText("player_bad.json", """{"Name":"Jack","Level":""");
        Player bad = LoadFromFile("player_bad.json");
        Console.WriteLine($"[Restored] Name: {bad.Name}, Level: {bad.Level}");
        
        // 3. Файл не існує
        Player missing = LoadFromFile("player_missing.json");
        Console.WriteLine($"[Restored] Name: {missing.Name}, Level: {missing.Level}");
    }
}