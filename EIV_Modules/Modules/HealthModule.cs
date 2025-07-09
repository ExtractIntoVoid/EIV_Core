namespace EIV_Modules.Modules;

public class HealthModule : BaseChangingModule<int>
{
    public HealthModule() : base(0, 100)
    {
        OnMinimum += Minimum;
    }

    public string LastCause { get; private set; }

    public void Damage(int value, string Cause)
    {
        LastCause = Cause;
        base.RemoveValue(value);
        OnDamage?.Invoke(value, Cause);
    }

    public void Heal(int value, bool EnableOverHeal)
    {
        base.AddValue(value, EnableOverHeal);
        OnHeal?.Invoke(value, EnableOverHeal);
    }

    private void Minimum()
    {
        OnDeath?.Invoke(LastCause);
    }

    public event Action<string> OnDeath;
    public event Action<int, string> OnDamage;
    public event Action<int, bool> OnHeal;
}
