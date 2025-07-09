#if false
using System.Collections.Generic;
using System.Linq;
using EIV_Modules;
using EIV_Modules.Extensions;

public class ExampleSub
{
    public static void Init()
    {
        ModuleSub.OnAddModule = (obj, imodule) => 
        { 
            if (obj is not ExampleGameObject go)
                return;
            go.AddModule(imodule);
        };
        ModuleSub.RemoveModule = (obj, type) => 
        {
            if (obj is not ExampleGameObject go)
                return;
            go.RemoveModule(type);
        };
        ModuleSub.GetModuleList = (obj, type) => 
        {
            if (obj is not ExampleGameObject go)
                return new();
            return go.GetModules(type);
        };
        ModuleSub.GetFirstModule = (obj, type) => 
        {
            if (obj is not ExampleGameObject go)
                return null;
            return go.GetModule(type);
        };
    }

}
#endif