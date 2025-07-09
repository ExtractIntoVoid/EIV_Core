using EIV_Modules.Extensions;
using EIV_Modules.Modules;

namespace EIV_Core;

public abstract class CorePlayer
{
    public string PlayerName { get; set; }
    public string UserId { get; set; }
    public string UserToken { get; set; }

    public HealthModule Health { get; private set; }
    public EnergyModule Energy { get; private set; }
    public HydrationModule Hydration { get; private set; }

    public void InitModules()
    {
        Health = this.AddAndGetModuleNode<HealthModule>();
        Energy = this.AddAndGetModuleNode<EnergyModule>();
        Hydration = this.AddAndGetModuleNode<HydrationModule>();
        this.AddAndGetModuleNode<EffectModule>();
        this.AddAndGetModuleNode<InventoryModule>();
    }
}