namespace PR_2_Events_ClimateControl;

public class SecuritySystem
{
    public void Subscribe(TemperatureSensor sensor)
    {
        sensor.TemperatureChanged += OnTemperatureChanged;
    }

    private void OnTemperatureChanged(object? sender, TemperatureChangedEventArgs e)
    {
        if (e.Temperature > 40)
        {
            Console.WriteLine("[SecuritySystem] НЕБЕЗПЕКА: Перегрів системи!");
        }
        else if (e.Temperature < 5)
        {
            Console.WriteLine("[SecuritySystem] ПОПЕРЕДЖЕННЯ: Ризик замерзання систем!");
        }
    }
}