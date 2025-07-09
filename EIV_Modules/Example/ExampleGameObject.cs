#if false
using System.Collections.Generic;
using System.Linq;
using EIV_Modules;

public class ExampleGameObject
{
    public List<IModule> Modules { get; } = new();
    
    public void AddModule(IModule module)
    {
        Modules.Add(module);
    }
    
    public List<IModule> GetModules(Type t)
    {
        return Modules.Where(x => x.GetType() == t).ToList();
    }

    public IModule? GetModule(Type t)
    {
        return Modules.FirstOrDefault(x => x.GetType() == t);
    }
    
    public void RemoveModule(Type t)
    {
        foreach (var m in GetModules(t))
        {
            Modules.Remove(m);
        }
    }
}
#endif