using HarmonyLib;
using HBS.Logging;
using System;

namespace ContractDifficultyMangler;

public static class ModInit
{
    internal static Logger modLog;

    public static void Init(string directory, string settings)
    {
        modLog = Logger.GetLogger(typeof(ModInit).Namespace);
        modLog.Log("I did the thing");

        // apply all patches that are in classes annotated with [HarmonyPatch]
        Harmony.CreateAndPatchAll(typeof(ModInit).Assembly);
    }
}
