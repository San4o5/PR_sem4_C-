namespace PR_2_Events_GameDamageSystem;

public class Player
{
    private static int _hp = 100;
    
    public event EventHandler<DamageReceivedEventArgs>? DamageReceived;

    public void TakeDamage(int damage)
    {
        _hp = Math.Max(0, _hp - damage);
        Console.WriteLine($"\nPlayer отримує {damage} урону");
        DamageReceived?.Invoke(this,new DamageReceivedEventArgs(damage,_hp));
    }
}