using System.Text.Json.Serialization;

namespace CircularReference;

public class Book
{
    public string Title { get; set; }
    [JsonIgnore] // розриває цикл Author → Books → Book.Author → Books → ...
    public Author Author { get; set; }
}