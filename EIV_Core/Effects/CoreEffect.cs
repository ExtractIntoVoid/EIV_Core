using EIV_Common.Coroutines;
using EIV_JsonLib;
using EIV_Modules.Modules;
using System.Collections.Generic;

namespace EIV_Core.Effects;

public abstract class CoreEffect(Effect effect, object parent)
{
    CoroutineHandle? TimeCoroutine;
    public Effect JsonEffect => effect;
    private object Parent => parent;

    public virtual void StartEffect()
    {
        StartEffect(JsonEffect.Time.Initial, JsonEffect.Strength.Min);
    }

    public virtual void StartEffect(double Seconds, int Strength)
    {
        TimeCoroutine = CoroutineStaticExt.StartCoroutine(TimeStuff(Seconds, Strength), "Effect");
    }

    public virtual void StopEffect()
    {
        if (TimeCoroutine == null)
            return;
        CoroutineStaticExt.KillCoroutineInstance(TimeCoroutine.Value);
    }

    public virtual void EffectTick(int Strength)
    {
        if (JsonEffect.Strength.ApplyTo.Contains("Health") && Parent.TryGetModule(out HealthModule healthModule))
        {
            healthModule.Damage(JsonEffect.Health.Negative * Strength, JsonEffect.Health.Cause);
            healthModule.Heal(JsonEffect.Health.Positive * Strength, true);
        }
        if (JsonEffect.Strength.ApplyTo.Contains("Energy") && Parent.TryGetModule(out EnergyModule energyModule))
        {
            energyModule.RemoveValue(JsonEffect.Energy.Negative * Strength);
            energyModule.AddValue(JsonEffect.Energy.Positive * Strength, false);
        }
    }

    private IEnumerator<double> TimeStuff(double InitialTime, int Strength)
    {
        yield return JsonEffect.Time.WaitUntilApply;
        double time = InitialTime;
        yield return CoroutineStaticExt.WaitUntilZero(
        () => 
        {
            EffectTick(Strength);
            time--;
            return time;
        });
        TimeCoroutine = null;
    }
}