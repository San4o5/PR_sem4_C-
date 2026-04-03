using System.Text.Json;

namespace StudentSerializer;

public class StudentService
{
    private const string FilePath = "/home/sedziro/RiderProjects/PR_4_SerializationDeserialization/StudentSerializer/students.json";

    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public void SerializeToFile(List<Student> students)
    {
        string json = JsonSerializer.Serialize(students, _options);
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Serialized {FilePath}");
        Console.WriteLine(json);
    }

    public List<Student>? DeserializeFromFile()
    {
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Student>>(json);
    }
    
    public void PrintStudents(List<Student> students)
    {
        foreach (Student s in students)
        {
            Console.WriteLine($"Name: {s.Name}\nAge: {s.Age}\nAverage score: {s.AverageScore}");
        }
    }
}      