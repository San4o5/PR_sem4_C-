namespace Module_1;

public class FileLogger
{
    private string _logPath;
    
    public FileLogger(string logPath, MessagePublisher publisher)
    {
        _logPath = logPath;
        publisher.MessageSent += OnMessageSent;
    }

    private void OnMessageSent(string message)
    {
        string time = DateTime.Now.ToString("HH:mm:ss");
        string entry = $"[{time}] {message}";

        using (StreamWriter streamWriter = File.AppendText(_logPath))
        {
            streamWriter.WriteLine(entry);
        }
    }
}