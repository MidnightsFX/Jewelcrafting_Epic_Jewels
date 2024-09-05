using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class WetWorker
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        private static int wetstatus = "Wet".GetStableHashCode();


        [HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
        public class ReduceStaminaCostWet_Patch
        {
            public static void Postfix(Attack __instance, ref float __result)
            {
                if (__instance.m_character is Player player)
                {
                    if (Player.m_localPlayer.GetEffectPower<Config>("Wet Worker").Power > 0)
                    {
                        bool thisVikingisWet = Player.m_localPlayer.GetSEMan().HaveStatusEffect(wetstatus);
                        if (thisVikingisWet)
                        {
                            float weapon_usage_stamina_multiplier = (100f / (player.GetEffectPower<Config>("Wet Worker").Power + 100f));
                            EpicJewels.EJLog.LogDebug($"Wet Worker Stamina Reduction multipler: {weapon_usage_stamina_multiplier}");
                            __result *= weapon_usage_stamina_multiplier;
                        }
                    }
                }
            }
        }
    }
}
