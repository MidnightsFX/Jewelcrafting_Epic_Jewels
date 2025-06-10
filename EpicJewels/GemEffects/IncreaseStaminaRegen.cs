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
    public static class IncreaseStaminaRegen
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyStaminaRegen))]
        public static class IncreasePlayerStaminaRegen
        {
            public static void Postfix(SEMan __instance, ref float staminaMultiplier)
            {
                if (__instance.m_character is Player player && player != null && player.GetEffectPower<Config>("Increase Stamina Regen").Power > 0)
                {
                    float stamina_multiplier = (player.GetEffectPower<Config>("Increase Stamina Regen").Power + 100) / 100;
                    // Super noisy
                    // EpicJewels.EJLog.LogDebug($"stamina regen multiplied by {stamina_multiplier}");
                    staminaMultiplier *= stamina_multiplier;
                }
            }
        }
    }
}
