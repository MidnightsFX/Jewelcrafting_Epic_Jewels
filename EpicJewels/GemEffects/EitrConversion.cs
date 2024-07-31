using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class EitrConversion
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [InverseMultiplicativePercentagePower] public float Chance;
        }

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class HealOnParry
        {
            private static void Postfix(Humanoid __instance, ref bool __result)
            {
                if (__instance is Player player && player.GetEffectPower<Config>("Eitr Conversion").Power > 0)
                {
                    float roll = Random.value;
                    float chance_max = (Player.m_localPlayer.GetEffectPower<Config>("Eitr Conversion").Chance / 100);
                    EpicJewels.EJLog.LogDebug($"Eitr Conversion chance roll: {roll} < {chance_max}");
                    if (roll < chance_max)
                    {
                        player.AddEitr(player.GetMaxEitr() * (player.GetEffectPower<Config>("Eitr Conversion").Power / 100f));
                    }
                }
            }
        }
    }
}
