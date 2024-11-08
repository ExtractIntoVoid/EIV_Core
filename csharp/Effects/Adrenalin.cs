using EIV_JsonLib.Interfaces;
using ExtractIntoVoid.Effects;
using ExtractIntoVoid.Menus;
using ExtractIntoVoid.Physics;
using Godot;

namespace EIV_Core.Effects;

public partial class Adrenalin : EffectBase
{
    public Adrenalin(IEffect effect, Node parentNode) : base(effect, parentNode)
    {
    }

    public override void EffectTick(int Strength)
    {
        base.EffectTick(Strength);
        if (this.ParentNode is Player player && player != null)
        {
            var ui = player.Camera.GetNode<MainUI>("MainUI");
            ui.SetShader(5, 0.2f, 0, 0.4f, 0);
        }
    }
}
