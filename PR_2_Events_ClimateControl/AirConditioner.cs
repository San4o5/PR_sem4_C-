namespace PR_2_Events_ClimateControl;

public class AirConditioner
{
    public void Subscribe(TemperatureSensor sensor)
    {
        sensor.TemperatureChanged += OnTemperatureChanged;
    }
    
    public void OnTemperatureChanged(object? sender, TemperatureChangedEventArgs e)
    {
        if (e.Temperature < 17)
        {
            Console.WriteLine("[AirConditioner] Режим: ОБІГРІВ увімкнено.");
        }
        else if (e.Temperature <= 25)
            Console.WriteLine("[AirConditioner] Режим: ВИМКНЕНО (комфортна температура).");
        else
            Console.WriteLine("[AirConditioner] Режим: ОХОЛОДЖЕННЯ увімкнено.");
    }
    
}