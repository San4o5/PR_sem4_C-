using System.Text.Json;

namespace TaskTracker;

public class TaskManager
{
    private const string FilePath = "/home/sedziro/RiderProjects/PR_4_SerializationDeserialization/TaskTracker/tasks.json";
    private List<TaskItem> _tasks = new List<TaskItem>();
    
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true
    };

    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("No tasks.json file found.");
            return;
        }
        
        string json = File.ReadAllText(FilePath);
        List<TaskItem>? loaded = JsonSerializer.Deserialize<List<TaskItem>>(json);

        if (loaded != null)
        {
            _tasks = loaded;
            Console.WriteLine($"Loaded {_tasks.Count} tasks.");
        }
    }

    public void Save()
    {
        string json = JsonSerializer.Serialize(_tasks, _jsonOptions);
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Saved {_tasks.Count} tasks.");
    }

    public void AddTask(string title)
    {
        _tasks.Add(new TaskItem { Title = title, IsCompleted = false });
        Console.WriteLine($"Added {title} tasks.");
    }

    public void ToggleStatus(int index)
    {
        if (index < 1 || index > _tasks.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }
        
        TaskItem task = _tasks[index - 1];
        task.IsCompleted = !task.IsCompleted;
        
        string status = task.IsCompleted ? "Completed" : "In Progress";
        Console.WriteLine($"Status task {task.Title} change to {status}");
    }
    
    public void ShowAll()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        for (int i = 0; i < _tasks.Count; i++)
        {
            string status = _tasks[i].IsCompleted ? "Completed" : "In Progress";
            Console.WriteLine($"{i + 1}. {status} {_tasks[i].Title}");
        }
    }
}