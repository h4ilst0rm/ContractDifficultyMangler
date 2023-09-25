using BattleTech.Data;
using BattleTech.Framework;
using HarmonyLib;

namespace ContractDifficultyMangler.Patches;

[HarmonyPatch(typeof(SimGame_QueryExstensions))]
static class SimGame_QueryExstensions_Patches
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SimGame_QueryExstensions.GetContractsByDifficultyRangeAndScopeAndOwnership))]
    public static void GetContractsByDifficultyRangeAndScopeAndOwnership_Prefix(ref int minDifficulty, ref int maxDifficulty)
    {
        ModInit.modLog?.Log("db query min: " + minDifficulty + " max: " + maxDifficulty);

        switch ((ContractDifficulty)minDifficulty)
        {
            case ContractDifficulty.Easy:
                minDifficulty = ModInit.modSettings.EasyContractDiff;
                break;
            case ContractDifficulty.Medium:
                minDifficulty = ModInit.modSettings.MediumContractDiff;
                break;
            case ContractDifficulty.Hard:
                minDifficulty = ModInit.modSettings.HardContractDiff;
                break;
            default:
                break;
        }

        switch ((ContractDifficulty)maxDifficulty)
        {
            case ContractDifficulty.Easy:
                maxDifficulty = ModInit.modSettings.EasyContractDiff;
                break;
            case ContractDifficulty.Medium:
                maxDifficulty = ModInit.modSettings.MediumContractDiff;
                break;
            case ContractDifficulty.Hard:
                maxDifficulty = ModInit.modSettings.HardContractDiff;
                break;
            default:
                break;
        }

        ModInit.modLog?.Log("set new min: " + minDifficulty + " max: " + maxDifficulty);
    }
}
