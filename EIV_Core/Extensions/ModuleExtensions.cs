using System.Linq;
using EIV_Core.Modules;

namespace EIV_Core.Extensions;

public static class ModuleExtensions
{
    public static T GetModule<T>(this object obj) where T : IModule
    {
        return (T?)ModuleSub.GetFirstModule?.Invoke(obj, typeof(T));
    }

    public static List<T> GetModules<T>(this object obj) where T : IModule
    {
        return (List<T>)ModuleSub.GetModuleList?.Invoke(obj, typeof(T)).Select(x => (T)x);
    }

    public static bool TryGetModule<T>(this object obj, out T out_t) where T : IModule
    {
        out_t = default;
        out_t = (T?)ModuleSub.GetFirstModule?.Invoke(obj, typeof(T));
        return out_t != null;
    }

    public static void AddModule<T>(this object obj) where T : IModule, new()
    {
        ModuleSub.AddModule?.Invoke(obj, new T());
    }

    public static T AddAndGetModule<T>(this object obj) where T : IModule, new()
    {
        T module = new();
        ModuleSub.AddModule?.Invoke(obj, module);
        return module;
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
