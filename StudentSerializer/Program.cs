namespace StudentSerializer;

class Program
{
    static void Main(string[] args)
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Olena", Age = 20, AverageScore = 4.8 },
            new Student { Name = "Maxim", Age = 21, AverageScore = 3.9 },
            new Student { Name = "Anya", Age = 19, AverageScore = 4.5 },
            new Student { Name = "Dmytro", Age = 22, AverageScore = 3.6 },
            new Student { Name = "Sonya", Age = 20, AverageScore = 4.2 }
        };
        
        StudentService service = new StudentService();
        
        service.SerializeToFile(students);

        List<Student>? loaded = service.DeserializeFromFile();

        if (loaded != null)
        {
            service.PrintStudents(loaded);
        }
    }
}