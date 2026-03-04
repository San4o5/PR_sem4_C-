namespace PR_2_Events_GameDamageSystem;

public class GameLogger
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
        Console.WriteLine($"[GameLogger] Урон: -{e.Damage} | Залишилось HP: {e.CurrentHp}");
    }
}
