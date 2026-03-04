namespace PR_2_Events_ClimateControl;

public class TemperatureChangedEventArgs : EventArgs
{
    public double Temperature { get; }

    public TemperatureChangedEventArgs(double temperature)
    {
        Temperature = temperature;
    }
}