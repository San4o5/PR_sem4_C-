namespace PR_3_FileIO;

public class ClearCacheTask4
{
    public void ClearCacheWithoutRecursion(string cachePath)
    {
        string[] files = Directory.GetFiles(cachePath, "*", SearchOption.AllDirectories);

        int count = 0;
        long totalSize = 0;

        foreach (string file in files)
        {
            FileInfo fi = new FileInfo(file);
            totalSize += fi.Length;
            File.Delete(file);
            count++;
        }

        Console.WriteLine($"Видалено файлів: {count}");
        Console.WriteLine($"Звільнено місця: {totalSize} байт");
    }
    
    public void ClearCacheWithRecursion(string cachePath, ref int count, ref long totalSize)
    {
        foreach (string file in Directory.GetFiles(cachePath))
        {
            FileInfo fi = new FileInfo(file);
            totalSize += fi.Length;
            File.Delete(file);
            count++;
        }

        foreach (string dir in Directory.GetDirectories(cachePath))
        {
            ClearCacheWithRecursion(dir, ref count, ref totalSize);
        }
    }
}