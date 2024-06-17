using EpicJewels.EffectHelpers;
using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicJewels.GemEffects
{
    public static class WeaponReducedStamina
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
        public class ReduceStaminaCostForAttack
        {
            public static void Postfix(Attack __instance, ref float __result)
            {
                if (__instance.m_character is Player player)
                {
                    if (Player.m_localPlayer.GetEffectPower<Config>("WeaponReducedStamina").Power > 0)
                    {
                        float weapon_usage_stamina_multiplier = 1 - (100 / player.GetEffectPower<Config>("WeaponReducedStamina").Power);
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"Stamina Reduction multipler: {weapon_usage_stamina_multiplier}"); }
                        __result *= weapon_usage_stamina_multiplier;
                    }
                }
            }
        }
    }
}
