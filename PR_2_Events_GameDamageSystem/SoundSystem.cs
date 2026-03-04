namespace PR_2_Events_GameDamageSystem;

public class SoundSystem
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
        Console.WriteLine($"[SoundSystem] Звук: отримання урону!");
        if (e.CurrentHp <= 20)
            Console.WriteLine($"[SoundSystem] Звук: КРИТИЧНИЙ СТАН!");
    }
}