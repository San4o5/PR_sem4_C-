namespace PR_3_FileIO;

public class FolderInspectorTask2
{
    public void InspectFolder(string folderPath)
    {
        Console.WriteLine($"Папка: {folderPath}\n");

        Console.WriteLine("Підпапки: ");
        foreach (string dir in Directory.GetDirectories(folderPath))
        {
            Console.WriteLine(Path.GetFileName(dir));
        }

        Console.WriteLine("\nФайли: ");
        foreach (string file in Directory.GetFiles(folderPath))
        {
            FileInfo fi = new FileInfo(file);
            Console.WriteLine($"{fi.Name} | {fi.Length} байт | створено: {fi.CreationTime}");
        }
    }
}