namespace PR_3_FileIO;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        TextAnalyzerTask1 task1 = new TextAnalyzerTask1();
        task1.StreamTask1();
        
        FolderInspectorTask2 task2 = new FolderInspectorTask2();
        task2.InspectFolder("/home/sedziro/VSCode/");
        
        LargestFileTask3 task3 = new LargestFileTask3();
        task3.FindLargestFile("/home/sedziro/RiderProjects/PR_3_FileIO/PR_3_FileIO");
        
        ClearCacheTask4 task4 = new ClearCacheTask4();

        // Без рекурсії
        task4.ClearCacheWithoutRecursion("/home/sedziro/.cache");

        // З рекурсією
        int count = 0;
        long totalSize = 0;
        task4.ClearCacheWithRecursion("/home/sedziro/.cache", ref count, ref totalSize);
        Console.WriteLine($"Видалено файлів: {count}");
        Console.WriteLine($"Звільнено місця: {totalSize} байт");
        
        FileAnalyzerTask5 task5 = new FileAnalyzerTask5();
        if (args.Length == 0)
        {
            Console.WriteLine("Вкажіть шлях до папки: analyzer /path/to/folder");
            return;
        }
        task5.Analyze(args[0]);
    }
}