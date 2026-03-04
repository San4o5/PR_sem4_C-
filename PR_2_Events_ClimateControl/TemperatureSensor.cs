namespace PR_2_Events_ClimateControl;

public class TemperatureSensor
{
    public double _temperature;
    
    public event EventHandler<TemperatureChangedEventArgs>? TemperatureChanged;

    public double Temperature
    {
        set
        {
            _temperature = value;
            OnTemperatureChanged(_temperature);
        }
    }

    protected void OnTemperatureChanged(double temperature)
    {
        TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(temperature));
    }
}