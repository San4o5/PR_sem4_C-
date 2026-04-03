using System.Text.Json;

namespace PolymorphismSerialization;

class Program
{
    static void Main(string[] args)
    {
        List<Animal> animals = new List<Animal>
        {
            new Dog { Name = "Reks", BarkVolume = 80 },
            new Cat { Name = "Myrka", Lives = 9 },
            new Dog { Name = "Baron", BarkVolume = 65 }
        };
        
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions{ WriteIndented = true };
        
        string json = JsonSerializer.Serialize(animals, jsonOptions);
        Console.WriteLine($"Serialized json: \n{json}");
        
        List<Animal>? loaded =  JsonSerializer.Deserialize<List<Animal>>(json, jsonOptions);
        Console.WriteLine($"After deserializer: ");
        foreach (Animal animal in loaded)
        {
            if (animal is Dog dog)
            {
                Console.WriteLine($"Dog: {dog.Name}, BarkVolume: {dog.BarkVolume}");
            }
            else if (animal is Cat cat)
            {
                Console.WriteLine($"Cat: {cat.Name}, Lives: {cat.Lives}");
            }
        }
        
    }
}