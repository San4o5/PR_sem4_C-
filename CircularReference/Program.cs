using System.Text.Json;

namespace CircularReference;

class Program
{
    static void Main(string[] args)
    {
        Author author = new Author { Name = "Taras" };
        
        author.Books.Add(new Book { Title = "The Dark Knight", Author = author });
        author.Books.Add(new Book { Title = "The White Knight", Author = author });
        author.Books.Add(new Book { Title = "The Pink Knight", Author = author });

        var options = new JsonSerializerOptions{WriteIndented=true};
        string json = JsonSerializer.Serialize(author, options);
        
        Console.WriteLine(json);
    }
}