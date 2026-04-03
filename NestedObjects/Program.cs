using System.Text.Json;

namespace NestedObjects;

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player
        {
            Name = "James",
            Inventory = new Inventory
            {
                Items = new List<string> { "Sword", "Shield", "Potion" }
            }
        };
        
        JsonSerializerOptions jsonOption = new JsonSerializerOptions{WriteIndented=true};
        
        string json = JsonSerializer.Serialize(player, jsonOption);
        Console.WriteLine($"Origin JSON: \n{json}");
        
        // Вручну видаляємо поле Inventory з JSON
        string jsonWithoutInventory = """
                                      {
                                          "Name": "Герой"
                                      }
                                      """;
        Console.WriteLine($"Json without inventory: \n{jsonWithoutInventory}");
        // Десеріалізація — Inventory буде null
        Player loaded = JsonSerializer.Deserialize<Player>(jsonWithoutInventory, jsonOption);
        // Обробка ситуації коли вкладений об'єкт null
        loaded.Inventory ??= new Inventory();
        
        Console.WriteLine($"After deserialization: ");
        Console.WriteLine($"Name: {loaded.Name}");
        Console.WriteLine($"Inventory items: {(loaded.Inventory.Items.Count == 0 ? "порожній" : string.Join(", ", loaded.Inventory.Items))}");
    }
}