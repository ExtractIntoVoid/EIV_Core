using System.Linq;

namespace EIV_Modules.Extensions;

public static class ModuleExtensions
{
    public static T GetModule<T>(this object obj) where T : IModule
    {
        return (T?)ModuleSub?.Invoke(obj, typeof(T));
    }

    public static bool TryGetModuleNode<T>(this object obj, out T out_t) where T : IModule
    {
        out_t = default;
        out_t = (T)node.GetChildren().FirstOrDefault(x => x is T);
        return out_t != null;
    }

    public static void AddModuleNode<T>(this object obj, T module) where T : IModule
    {
        module.SetMultiplayerAuthority(node.GetMultiplayerAuthority());
        node.AddChild(module);
    }

    public static void RemoveModuleNode<T>(this object obj) where T : IModule
    {
        var NodeList = node.GetChildren().Where(x=>x is T).ToList();
        foreach(var Node in NodeList)
        {
            node.RemoveChild(Node);
        }
    }
}
