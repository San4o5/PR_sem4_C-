namespace PR_3_FileIO;

public class LargestFileTask3
{
    public void FindLargestFile(string folderPath)
    {
        string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

        FileInfo largest = null!;

        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (largest == null! || fileInfo.Length > largest.Length)
            {
                largest = fileInfo;
            }
        }

        if (largest == null!)
        {
            Console.WriteLine("Файлів не знайдено.");
            return;
        }
        Console.WriteLine($"Name: {largest.Name}");
        Console.WriteLine($"Size: {largest.Length} байт");
        Console.WriteLine($"Path: {largest.FullName}");
    }
}