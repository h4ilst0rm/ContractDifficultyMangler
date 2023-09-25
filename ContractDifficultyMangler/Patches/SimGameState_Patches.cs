using BattleTech;
using BattleTech.Framework;
using HarmonyLib;

namespace ContractDifficultyMangler.Patches;

[HarmonyPatch(typeof(SimGameState))]
static class SimGameState_Patches
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(SimGameState.GetDifficultyEnumFromValue))]
    public static void GetDifficultyEnumFromValue_Postfix(ref ContractDifficulty __result, int value)
    {
        if (value < ModInit.modSettings.MediumContractStartDiff)
        {
            __result = ContractDifficulty.Easy;
        }
        else if (value < ModInit.modSettings.HardContractStartDiff)
        {
            __result = ContractDifficulty.Medium;
        }
        else
        {
            __result = ContractDifficulty.Hard;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(SimGameState.IsWithinDifficultyRange))]
    private static void IsWithinDifficultyRange_Postfix(ref bool __result, SimGameState.ContractDifficultyRange diffRange, ContractDifficulty ovrDiff)
    {
        __result = true;
    }

    /*
    // used for testing that contract selection remained the same with/without the IsWithinDifficultyRange_Postfix
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SimGameState.StartGeneratePotentialContractsRoutine))]
    public static void StartGeneratePotentialContractsRoutine_Prefix(SimGameState __instance)
    {
        SimGameState.ContractDifficultyRange EasyRange = new SimGameState.ContractDifficultyRange(2, 2, ContractDifficulty.Easy, ContractDifficulty.Easy);
        SimGameState.ContractDifficultyRange MediumRange = new SimGameState.ContractDifficultyRange(5, 5, ContractDifficulty.Medium, ContractDifficulty.Medium);
        SimGameState.ContractDifficultyRange HardRange = new SimGameState.ContractDifficultyRange(8, 8, ContractDifficulty.Hard,ContractDifficulty.Hard);

        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(EasyRange), nameof(EasyRange));
        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(MediumRange), nameof(MediumRange));
        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(HardRange), nameof(HardRange));

        jemma = true;

        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(EasyRange), nameof(EasyRange) + "_after");
        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(MediumRange), nameof(MediumRange) + "_after");
        WriteJson(__instance.GetSinglePlayerProceduralContractOverrides(HardRange), nameof(HardRange) + "_after");
    }

    public static void WriteJson(Dictionary<int, List<ContractOverride>> list, string name)
    {
        var result = list.ToDictionary(kp => kp.Key, kp => kp.Value.Select(contract => contract.ID).ToList());
        string json = JsonConvert.SerializeObject(result, Formatting.Indented);
        File.WriteAllText(name + ".json", json);
    }
    */
}
