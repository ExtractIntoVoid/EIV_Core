using System.Linq;

namespace EIV_Modules.Extensions;

public static class ModuleSub
{
    public static Action<object, IModule> AddModule;
    public static Action<object, Type> RemoveModule;
    public static Func<object, Type, List<IModule>> GetModuleList;
    public static Func<object, Type, IModule> GetFirstModule;
}