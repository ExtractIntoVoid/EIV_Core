using ExtractIntoVoid.Managers;
using ExtractIntoVoid.Modding;

namespace EIV_Core;

public class Init
{
    public static void OnInit(InitEvent initEvent)
    {
        if (initEvent.Disable)
            return;

        GameManager.Instance.logger.Information("EIV Core Initialized!");
    }
}
