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
    public static class BlockReduceStamina
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        public static class ModifyBlockStaminaUse_Humanoid_BlockAttack_Patch
        {
            public static void Prefix(Humanoid __instance, HitData hit, Character attacker)
            {
                if (__instance.IsPlayer() && Player.m_localPlayer.GetEffectPower<Config>("Block Reduce Stamina").Power > 0)
                {
                    float block_stamina_multiplier = 100f / (100f + Player.m_localPlayer.GetEffectPower<Config>("Block Reduce Stamina").Power);
                    EpicJewels.EJLog.LogDebug($"Multiplying block stamina cost by {block_stamina_multiplier}");
                    __instance.m_blockStaminaDrain *= block_stamina_multiplier;
                }
            }
        }
    }
}
