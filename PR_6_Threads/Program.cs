namespace PR_6_Threads;

class Program
{
    // Shared stats
    private static int _counter = 0;
    private static bool _paused = false;
    private static bool _running = true;

    private static readonly Lock _lock = new Lock();

    private static readonly ConsoleColor[] _colors =
    {
        ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.DarkMagenta,
        ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Magenta,
        ConsoleColor.Blue, ConsoleColor.White
    };

    private static int _colorIndex = 0;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        PrintHelp();

        // Фоновий потік - обробка клавіш
        Thread inputThread = new Thread(HandleInput)
        {
            IsBackground = true,
            Name = "InputThread"
        };

        inputThread.Start();

        // Основний потік - лічильник
        while (true)
        {
            bool shouldRun;
            lock (_lock) shouldRun = _running;
            if (!shouldRun) break;

            bool paused;
            lock (_lock) paused = _paused;
            if (!paused)
            {
                int value;
                lock (_lock) value = ++_counter;

                ConsoleColor color;
                lock (_lock) color = _colors[_colorIndex];

                Console.ForegroundColor = color;
                Console.WriteLine($"Counter: {value}");
                Console.ResetColor();
            }

            Thread.Sleep(1000);
        }

        Console.ResetColor();
        Console.WriteLine("\nПрограму завершено!");
    }

    static void HandleInput()
    {
        while (true)
        {
            bool shouldRun;
            lock (_lock) shouldRun = _running;
            if (!shouldRun) break;

            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50); // не блокуємо, просто чекаємо
                continue;
            }

            var key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.P:
                    lock (_lock) _paused = !_paused;
                    Console.WriteLine(_paused ? "[Пауза]" : "[Продовження]");
                    break;

                case ConsoleKey.R:
                    lock (_lock) _counter = 0;
                    Console.WriteLine("[Лічильник скинуто]");
                    break;

                case ConsoleKey.C:
                    lock (_lock) _colorIndex = (_colorIndex + 1) % _colors.Length;
                    Console.WriteLine("[Колір змінено]");
                    break;

                case ConsoleKey.Q:
                    lock (_lock) _running = false;
                    return;
            }
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("=== Лічильник з біндами ===");
        Console.WriteLine("P - Пауза / Продовження");
        Console.WriteLine("R - Скидання лічильника");
        Console.WriteLine("C - Зміна кольору");
        Console.WriteLine("Q - Вихід");
        Console.WriteLine("===========================\n");
    }
    
}