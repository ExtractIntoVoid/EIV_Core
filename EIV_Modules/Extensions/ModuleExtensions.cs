using System.Linq;

namespace EIV_Modules.Extensions;

public static class ModuleExtensions
{
    public static T GetModule<T>(this object obj) where T : IModule
    {
        return (T?)ModuleSub.GetFirstModule?.Invoke(obj, typeof(T));
    }

    public static List<T> GetModules<T>(this object obj) where T : IModule
    {
        return (List<T>)ModuleSub.GetModuleList?.Invoke(obj, typeof(T));
    }

    public static bool TryGetModule<T>(this object obj, out T out_t) where T : IModule
    {
        out_t = default;
        out_t = (T?)ModuleSub.GetFirstModule?.Invoke(obj, typeof(T));
        return out_t != null;
    }

    public static void AddModule<T>(this object obj, T module) where T : IModule
    {
        ModuleSub.AddModule?.Invoke(obj, module);
    }

    public static void RemoveModule<T>(this object obj) where T : IModule
    {
        ModuleSub.RemoveModule?.Invoke(obj, typeof(T));
    }
}
