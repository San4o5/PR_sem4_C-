namespace PR_3_FileIO;

public class TextAnalyzerTask1
{
    private int lines = 0;
    private int words = 0;
    private int chars = 0;
    
    public void StreamTask1()
    {
        string filePath = "/home/sedziro/RiderProjects/PR_3_FileIO/PR_3_FileIO/story.txt";
        using (StreamReader sr = new StreamReader(filePath))
        {
            string content = sr.ReadToEnd();
            chars = content.Length;
            lines = content.Split('\n').Length;
            words = content.Split(new char[] { ' ', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }
        
        using (StreamWriter sw = new StreamWriter("/home/sedziro/RiderProjects/PR_3_FileIO/PR_3_FileIO/report.txt"))
        {
            sw.WriteLine($"Рядків: {lines}");
            sw.WriteLine($"Слів: {words}");
            sw.WriteLine($"Символів: {chars}");
        }
    }
}