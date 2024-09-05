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
                if (__instance.GetSEMan().HaveStatusEffect(wetstatus) || __instance.GetEffectPower<Waterproof.Config>("Waterproof").Power > 0 && (__instance.GetEffectPower<Config>("Water Swiftness").Power > 0 || __instance.GetEffectPower<Config>("Slippery When Wet").Power > 0))
                {
                    float speed_modifier = ((__instance.GetEffectPower<Config>("Water Swiftness").Power + __instance.GetEffectPower<Config>("Slippery When Wet").Power + 100f) / 100f);
                    // EpicJewels.EJLog.LogDebug($"Wet jog speed modifier: {speed_modifier}");
                    __result *= speed_modifier;
                }
                
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetRunSpeedFactor))]
        private class IncreaseRunSpeed
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                if (__instance.GetSEMan().HaveStatusEffect(wetstatus) || __instance.GetEffectPower<Waterproof.Config>("Waterproof").Power > 0 && (__instance.GetEffectPower<Config>("Water Swiftness").Power > 0 || __instance.GetEffectPower<Config>("Slippery When Wet").Power > 0))
                {
                    float speed_modifier = ((__instance.GetEffectPower<Config>("Water Swiftness").Power + __instance.GetEffectPower<Config>("Slippery When Wet").Power + 100f) / 100f);
                    // EpicJewels.EJLog.LogDebug($"Wet run speed modifier: {speed_modifier}");
                    __result *= speed_modifier;
                }
            }
        }
    }
}
