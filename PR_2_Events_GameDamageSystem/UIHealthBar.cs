namespace PR_2_Events_GameDamageSystem;

public class UiHealthBar
{
    public void Subscribe(Player player)
    {
        player.DamageReceived += OnDamageReceived;
    }

    public void Unsubscribe(Player player)
    {
        player.DamageReceived -= OnDamageReceived;
    }

    private void OnDamageReceived(object? sender, DamageReceivedEventArgs e)
    {
        Console.WriteLine($"[UIHealthBar] HP: {e.CurrentHp}/100");
    }
}