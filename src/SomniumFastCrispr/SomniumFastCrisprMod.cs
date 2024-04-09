using HarmonyLib;
using ModLoader;
using System;

namespace SomniumFastCrispr
{
    public class SomniumFastCrisprMod : IMod
    {
        Harmony HarmonyInstance;
        IModHelper Helper;

        public void ModEntry(IModHelper helper)
        {
            Helper = helper;
            HarmonyInstance = new Harmony("SomniumTD.SomniumFastCrispr");
            HarmonyInstance.PatchAll();

            // Using AccessTools and manual patching instead of Harmony annotations because the "CRISPRCalculator" class has visibility "internal"
            var type = Type.GetType("TinyZoo.Z_BalanceSystems.Animals.CRISPR.CRISPRCalculator, LetsBuildAZoo");
            HarmonyInstance.Patch(
                    original: AccessTools.Method(type, "GetDaysForThisCRISPRBreed"),
                    postfix: new HarmonyMethod(GetType(), nameof(CrisperOverride))
                    );
        }

        public static void CrisperOverride(ref float __result)
        {
            __result = 1.0f; // Override CRISPR complexity calculation to always return a duration of 1 day
        }

    }
}
