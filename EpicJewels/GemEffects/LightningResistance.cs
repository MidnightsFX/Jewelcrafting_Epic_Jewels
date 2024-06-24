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
    public static class LightningResistance
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
        public static class ReducePierceDamageTaken
        {
            [UsedImplicitly]
            private static void Prefix(Character __instance, HitData hit)
            {
                if (__instance is Player player && hit.GetAttacker() is { } attacker && attacker != __instance)
                {
                    if (hit.m_damage.m_lightning > 0)
                    {
                        if (player.GetEffectPower<Config>("LightningResistance").Power > 0)
                        {
                            float dmg_reduce = ((100f - player.GetEffectPower<Config>("LightningResistance").Power) / 100f);
                            EpicJewels.EJLog.LogDebug($"Lightning Resistance is reducing lightning damage {(1 - dmg_reduce)}");
                            hit.m_damage.m_fire *= dmg_reduce;
                        }
                    }
                }
            }
        }
    }
}
