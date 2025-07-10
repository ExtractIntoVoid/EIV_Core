using EIV_Core.Effects;
using EIV_JsonLib;

namespace EIV_Core.Api.Events;

public static class EffectEvents
{
    public static event Func<Effect, object, CoreEffect> OnMakeEffect;

    public static CoreEffect MakeEffect(Effect effect, object obj)
    {
        return OnMakeEffect?.Invoke(effect, obj);
    }
}