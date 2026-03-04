namespace PR_2_Events_GameDamageSystem;

public class DamageReceivedEventArgs : EventArgs
{
    public int Damage { get; }
    public int CurrentHp { get; }

    public DamageReceivedEventArgs(int damage, int currentHp)
    {
        Damage = damage;
        CurrentHp = currentHp;
    }
}