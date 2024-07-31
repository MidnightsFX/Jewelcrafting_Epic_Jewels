using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class BurningViking
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }
        private static int burningstatus = "Burning".GetStableHashCode();

        [HarmonyPatch(typeof(Player), nameof(Player.GetJogSpeedFactor))]
        private class IncreaseJogSpeed
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                bool thisVikingisOnFIRE = Player.m_localPlayer.GetSEMan().HaveStatusEffect(burningstatus);
                // EJLog.LogInfo($"Walking check for burning viking {thisVikingisOnFIRE}");
                if (thisVikingisOnFIRE && __instance.GetEffectPower<Config>("Burning Viking").Power > 0)
                {
                    __result *= ((__instance.GetEffectPower<Config>("Burning Viking").Power + 100) / 100f);
                }

            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetRunSpeedFactor))]
        private class IncreaseRunSpeed
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                bool thisVikingisOnFIRE = Player.m_localPlayer.GetSEMan().HaveStatusEffect(burningstatus);
                // EJLog.LogInfo($"Running check for burning viking {thisVikingisOnFIRE}");
                if (thisVikingisOnFIRE && __instance.GetEffectPower<Config>("Burning Viking").Power > 0)
                {
                    __result *= ((__instance.GetEffectPower<Config>("Burning Viking").Power + 100) / 100f);
                }
            }
        }
    }
}
