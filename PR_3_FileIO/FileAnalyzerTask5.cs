namespace PR_3_FileIO;

public class FileAnalyzerTask5
{
    public void Analyze(string folderPath)
    {
        int folders = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories).Length;
        string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

        long totalSize = 0;
        FileInfo largest = null!;

        foreach (string file in files)
        {
            FileInfo fi = new FileInfo(file);
            totalSize += fi.Length;
            if (largest == null! || fi.Length > largest.Length)
                largest = fi;
        }

        Console.WriteLine($"Folders: {folders}");
        Console.WriteLine($"Files: {files.Length}");
        Console.WriteLine($"Total size: {totalSize / (1024.0 * 1024):F2} MB");
        Console.WriteLine($"Largest file: {largest?.Name ?? "немає"}");
    }
}