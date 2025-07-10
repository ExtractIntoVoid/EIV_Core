using System.Linq;
using EIV_Core.Modules;

namespace EIV_Core.Extensions;

public static class ModuleSub
{
    public static Action<object, IModule> AddModule;
    public static Action<object, Type> RemoveModule;
    public static Func<object, Type, List<IModule>> GetModuleList;
    public static Func<object, Type, IModule> GetFirstModule;
}