using System.Text.Json;

namespace PR_5_DataLibrary.Infrastructure;

public class FileStorageProvider
{
    // Root folder where all JSON files are stored
    private readonly string _basePath;
    
    public FileStorageProvider(string basePath)
    {
        _basePath = basePath;
        
        // Create folder if it doesn't exist yet
        Directory.CreateDirectory(_basePath);
    }
    
    // Reads file contents, returns empty array if file missing
    public async Task<string> ReadAsync(string fileName)
    {
        var path = Path.Combine(_basePath, fileName);
        if  (!File.Exists(path))
            return "[]";
        
        return await File.ReadAllTextAsync(path);
    }
    
    // Write content to file, overwrites existing 
    public async Task WriteAsync(string fileName, string content)
    {
        var path = Path.Combine(_basePath, fileName);
        await File.WriteAllTextAsync(path, content);
    }
}