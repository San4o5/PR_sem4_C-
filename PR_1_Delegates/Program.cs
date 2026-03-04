namespace PR_1_Delegates;

class Program
{
    // Завдання 1
    //public delegate double MathOperation(double a, double b);
    static double Add(double a, double b) => a + b;
    static double Subtract(double a, double b) => a - b;
    static double Multiply(double a, double b) => a * b;
    static double Divide(double a, double b) => a / b;

    // Завдання 2
    public delegate void NotificationHandler(string message);

    static void SendEmail(string message) => Console.WriteLine($"Email sent: {message}");
    static void SendSms(string message) => Console.WriteLine($"SMS sent: {message}");

    // Завдання 3
    public delegate bool FilterPredicate(int number);

    static void FilterArray(int[] numbers, FilterPredicate predicate)
    {
        foreach (var item in numbers)
        {
            if (predicate(item))
                Console.WriteLine(item);
        }
    }
    static bool IsEven(int number) => number % 2 == 0;
    static bool IsGreaterThanFive(int number) => number > 5;

    // Завдання 4
    static List<string> students = new List<string>
    {
        "Андрій", "Богдан", "Аліна", "Василь", "Антон", "Дмитро", "Борис"
    };

    private static readonly char Letter = 'А'; 

    // Завдання 6
    public delegate bool Validator(string input);

    static Validator GetValidator(int minLength)
    {
        return input => input.Length >= minLength;
    }
    
    
    static void Main(string[] args) 
    {
        // Вивід завдання 1
        //MathOperation operation
        Func<double, double, double> operation;

        operation = Add;
        Console.WriteLine(operation(10, 20));

        operation = Subtract;
        Console.WriteLine(operation(10, 20));

        operation = Multiply;
        Console.WriteLine(operation(10, 20));

        operation = Divide;
        Console.WriteLine(operation(10, 20));

        // Вивід завдання 2
        NotificationHandler notification = SendEmail;
        notification += SendSms;
        notification("Hello World!");

        // Вивід завдання 3
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("IsEven:");
        FilterArray(numbers, IsEven);

        Console.WriteLine("Numbers > 5:");
        FilterArray(numbers, IsGreaterThanFive);

        Console.WriteLine("IsOdd:");
        FilterArray(numbers, n => n % 2 != 0);

        // Вивід завдання 4
        List<string> filtered = students.FindAll(name => name.StartsWith(Letter));
        Console.WriteLine($"Імена на літеру '{Letter}':");
        filtered.ForEach(Console.WriteLine);
        
        // Вивід завдання 5
        Logger logger = new Logger();
        logger.LogHandler = message => Console.WriteLine($"[LOG]: {message}");

        logger.Log("Програма запущена");
        logger.Log("Завантаження даних...");

        Console.WriteLine("--- Змінюємо LogHandler ---");
        logger.LogHandler = message => Console.WriteLine($"[LOG]: {message.ToUpper()}");

        logger.Log("Програма запущена");
        logger.Log("Завантаження даних...");
        
        // Вивід завдання 6
        Validator passwordValidator = GetValidator(8);
        Validator loginValidator = GetValidator(3);

        Console.Write("Введіть логін: ");
        string login = Console.ReadLine()!;

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine()!;

        Console.WriteLine();

        Console.WriteLine($"Логін '{login}': {(loginValidator(login) ? "✓ Валідний" : "✗ Занадто короткий (мін. 3 символи)")}");
        Console.WriteLine($"Пароль '{password}': {(passwordValidator(password) ? "✓ Валідний" : "✗ Занадто короткий (мін. 8 символів)")}");
    }
}