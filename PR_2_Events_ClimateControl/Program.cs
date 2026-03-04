namespace PR_2_Events_ClimateControl;

class Program
{
    static void Main(string[] args)
    {
        
        TemperatureSensor sensor = new TemperatureSensor();
        Display display = new Display();
        AirConditioner airConditioner = new AirConditioner();
        SecuritySystem securitySystem = new SecuritySystem();
        
        display.Subscribe(sensor);
        airConditioner.Subscribe(sensor);
        securitySystem.Subscribe(sensor);
        
        
        double[] temperatures = { 3, 15, 22, 27, 42 };
        foreach (double temp in temperatures)
        {
            Console.WriteLine($"\nВстановлення температури: {temp}°C");
            sensor.Temperature = temp;
        }
    }
}