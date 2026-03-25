namespace Module_1; // Task 1

public class FileProcessor
{
    public static void ProcessFile(string inputPath, string outputPath, TextOperation operation)
    {
        string[] lines = File.ReadAllLines(inputPath);

        using StreamWriter streamWriter = new StreamWriter(outputPath);

        foreach (string line in lines)
        {
            string result = operation(line);
            streamWriter.WriteLine(result);
        }
    }
    
}