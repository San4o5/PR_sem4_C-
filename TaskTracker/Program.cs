namespace TaskTracker;

class Program
{
    static void Main(string[] args)
    {
        TaskManager manager = new TaskManager();
        manager.Load();
        
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. Change the status of a task");
            Console.WriteLine("3. View all tasks");
            Console.WriteLine("4. Exit");
            
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Task name: ");
                    string? title = Console.ReadLine();
                    if (title != null)
                    {
                        manager.AddTask(title);
                    }
                    break;
                case "2":
                    manager.ShowAll();
                    Console.Write("Enter the task number to change the status: ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        manager.ToggleStatus(result);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    break;
                case "3":
                    manager.ShowAll();
                    break;
                case "4":
                    manager.Save();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Unknown option. Please try again");
                    break;
            }
                
            Console.WriteLine();
        }
    }
}