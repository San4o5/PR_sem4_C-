namespace Modul_1; //Task 1

public delegate string TextOperation(string line);
public class TextOperations
{
    public static string ToUpperCase(string line)
    {
        return line.ToUpper();
    }

    public static string CountChars(string line)
    {
        return $"Chars: {line.Length}";
    }
    
    public static string CountWords(string line)
    {
        string[] words = line.Split(' ');
        int count = 0;

        foreach (string word in words)
        {
            if (word != "")
            {
                count++;
            }
        }

        return $"Words: {count}";
    }
}