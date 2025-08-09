using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class Retribution
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [InverseMultiplicativePercentagePower] public float Chance;
        }

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class Retribution_Patch
        {
            static HitData originalHit = null;
            private static void Prefix(HitData hit)
            {
                originalHit = new HitData( damage: hit.GetTotalDamage());
            }

            private static void Postfix(Humanoid __instance, HitData hit, Character attacker, ref bool __result)
            {
                if (__instance is Player player && player.GetEffectPower<Config>("Retribution").Power > 0 && __result == true && attacker.IsDead() == false)
                {
                    float roll = Random.value;
                    float chance_max = (player.GetEffectPower<Config>("Retribution").Chance / 100);
                    // EpicJewels.EJLog.LogDebug($"Retribution chance roll: {roll} < {chance_max}");
                    if (roll < chance_max)
                    {
                        HitData retribution_hit = new HitData();
                        float hit_dmg = originalHit.GetTotalDamage();
                        retribution_hit.m_damage.m_damage = hit_dmg * (player.GetEffectPower<Config>("Retribution").Power / 100f);
                        // EpicJewels.EJLog.LogDebug($"Hit dmg {hit_dmg} Retribution returning damage {retribution_hit.m_damage.m_damage}");
                        attacker.Damage(retribution_hit);
                    }  
                }
            }
        }
    }
}
