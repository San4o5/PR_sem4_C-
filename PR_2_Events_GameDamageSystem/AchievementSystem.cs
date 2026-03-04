namespace PR_2_Events_GameDamageSystem;

public class AchievementSystem
{
    private bool _halfHealthUnlocked = false;
    private bool _firstDeathUnlocked = false;

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
        if (e.CurrentHp <= 50 && !_halfHealthUnlocked)
        {
            _halfHealthUnlocked = true;
            Console.WriteLine("[AchievementSystem] Досягнення отримано: \"Half Health\"");
        }

        if (e.CurrentHp <= 0 && !_firstDeathUnlocked)
        {
            _firstDeathUnlocked = true;
            Console.WriteLine("[AchievementSystem] Досягнення отримано: \"First Death\"");
        }
    }
}