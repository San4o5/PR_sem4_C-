namespace Module_1;

class Program
{
    static void Main(string[] args)
    {
        // Task 1
        string inputFile = "/home/sedziro/RiderProjects/Module_1/Module_1/textPD23.txt"; 
        string outputFile = "/home/sedziro/RiderProjects/Module_1/Module_1/resultPD23.txt";
        
        File.WriteAllText(outputFile, "");

        FileProcessor.ProcessFile(inputFile, outputFile, TextOperations.ToUpperCase);
        FileProcessor.ProcessFile(inputFile, outputFile, TextOperations.CountChars);
        FileProcessor.ProcessFile(inputFile, outputFile, TextOperations.CountWords);

        //Task 2
        string logFile = "/home/sedziro/RiderProjects/Module_1/Module_1/logPD23.txt";
        
        MessagePublisher publisher = new MessagePublisher();
        FileLogger logger = new FileLogger(logFile, publisher);

        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"Enter your message {i}: ");
            string? input = Console.ReadLine();
            if (input != null) 
                publisher.Send(input);
        }
        
    }
}