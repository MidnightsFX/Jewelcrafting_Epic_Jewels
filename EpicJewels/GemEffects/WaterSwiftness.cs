using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class WaterSwiftness
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }
        private static int wetstatus = "Wet".GetStableHashCode();

        [HarmonyPatch(typeof(Player), nameof(Player.GetJogSpeedFactor))]
        private class IncreaseJogSpeed
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                if (__instance.GetSEMan().HaveStatusEffect(wetstatus) && __instance.GetEffectPower<Config>("WaterSwiftness").Power > 0)
                {
                    __result *= ((__instance.GetEffectPower<Config>("WaterSwiftness").Power + 100f) / 100f);
                }
                
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetRunSpeedFactor))]
        private class IncreaseRunSpeed
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                if (__instance.GetSEMan().HaveStatusEffect(wetstatus) && __instance.GetEffectPower<Config>("WaterSwiftness").Power > 0)
                {
                    __result *= ((__instance.GetEffectPower<Config>("WaterSwiftness").Power + 100f) / 100f);
                }
            }
        }
    }
}
