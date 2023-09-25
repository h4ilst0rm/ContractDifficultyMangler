using HarmonyLib;
using HBS.Logging;
using Newtonsoft.Json;

namespace ContractDifficultyMangler;

public class Settings
{
    public bool logDebug = false;

    public int EasyContractDiff = 2;
    public int MediumContractDiff = 5;
    public int HardContractDiff = 8;

    public int MediumContractStartDiff = 4;
    public int HardContractStartDiff = 7;
}

public static class ModInit
{
    public static Settings modSettings;
    internal static ILog modLog;

    public static void Init(string settings)
    {
        modSettings = JsonConvert.DeserializeObject<Settings>(settings);

        modLog = modSettings.logDebug ? Logger.GetLogger(typeof(ModInit).Namespace) : null;

        // apply all patches that are in classes annotated with [HarmonyPatch]
        Harmony.CreateAndPatchAll(typeof(ModInit).Assembly);
    }
}
