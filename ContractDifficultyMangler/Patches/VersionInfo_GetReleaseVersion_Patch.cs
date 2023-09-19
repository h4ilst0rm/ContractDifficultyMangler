using HarmonyLib;
using HBS.Logging;

namespace ContractDifficultyMangler.Patches;

[HarmonyPatch(typeof(VersionInfo), nameof(VersionInfo.GetReleaseVersion))]
static class VersionInfo_GetReleaseVersion_Patch
{
    [HarmonyPostfix]
    [HarmonyAfter("io.github.mpstark.ModTek")]
    static void Postfix(ref string __result)
    {
        var old = __result;
        __result = old + "\nMTWHX(1)";

        ModInit.modLog.Log("I have logged something");
    }
}
