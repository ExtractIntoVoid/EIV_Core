using EIV_Common.JsonStuff;
using EIV_JsonLib;
using EIV_JsonLib.Base;
using EIV_JsonLib.Extension;
using EIV_Core.Effects;
using System.Collections.Generic;
using System.Linq;
using EIV_Core.Api.Events;

namespace EIV_Core.Modules;

public partial class EffectModule : IModule
{
    internal List<CoreEffect> Effects = [];

    public void ApplyEffectFromItem(CoreItem item)
    {
        var sideEffects = item.GetProperty<List<SideEffect>>("SideEffects");
        if (sideEffects.Count == 0)
            return;
        foreach (var sideEffect in sideEffects)
        {
            EffectApply(sideEffect, item);
        }
    }

    public void EffectApply(SideEffect sideEffect, CoreItem item)
    {
        var effect = EffectMaker.MakeNewEffect(sideEffect.EffectName);
        if (effect == null)
            return;
        // Cannot Apply from it.
        if (!effect.AppliedFrom.Contains(item.Id) || effect.AppliedFrom.Contains(item.ItemType))
            return;

        if (effect.Strength.Max < sideEffect.EffectStrength)
            return;
        if (effect.Strength.Min > sideEffect.EffectStrength)
            return;

        EffectApply(effect, sideEffect.EffectTime, sideEffect.EffectStrength);
    }


    public void EffectApply(string EffectName)
    {
        var effect = EffectMaker.MakeNewEffect(EffectName);
        if (effect == null)
            return;
        EffectApply(effect, effect.Time.Initial, effect.Strength.Min);
    }

    public void EffectApply(Effect effect, double time, int strength)
    {
        if (effect == null)
            return;

        var effectBase = EffectEvents.MakeEffect(effect, this);
        if (effectBase == null)
            return;
        effectBase.StartEffect(time, strength);
        Effects.Add(effectBase);
    }

    public void DisableEffect(string EffectName)
    {
        var effect = Effects.SingleOrDefault(x => x.CoreEffect.EffectName == EffectName);
        if (effect == null)
            return;
        effect.StopEffect();
        Effects.Remove(effect);
    }

    public List<string> GetEffectNames() => Effects.Select(x => x.CoreEffect.EffectName).ToList();

}
