using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    internal class FreezingGuard
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [AdditivePowerAttribute] public float Chance;
        }

        [HarmonyPriority(Priority.High)]
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class FreezingBlock_Patch
        {
            static HitData originalHit = null;
            private static void Prefix(HitData hit)
            {
                originalHit = new HitData(damage: hit.GetTotalDamage());
            }

            private static int froststatus = "Frost".GetStableHashCode();
            private static void Postfix(Humanoid __instance, HitData hit, Character attacker, ref bool __result)
            {
                if (__instance is Player player && player.GetEffectPower<Config>("Freezing Guard").Chance > 0 && __result == true && attacker.IsDead() == false)
                {
                    float roll = UnityEngine.Random.value;
                    float chance_max = (player.GetEffectPower<Config>("Freezing Guard").Chance / 100);
                    // EpicJewels.EJLog.LogDebug($"Freezing guard chance roll: {roll} < {chance_max}");
                    if (roll < chance_max)
                    {
                        HitData frost_rebuke_hit = new HitData();
                        frost_rebuke_hit.m_damage.m_frost = (player.GetEffectPower<Config>("Freezing Guard").Power / 100) * originalHit.m_damage.GetTotalDamage();
                        // EpicJewels.EJLog.LogDebug($"Hit dmg {originalHit} FreezingGuard returning damage {frost_rebuke_hit.m_damage.m_frost}");
                        attacker.Damage(frost_rebuke_hit);
                        attacker.m_seman.AddStatusEffect(froststatus, true, 3, player.m_skills.GetSkill(Skills.SkillType.Blocking).m_level);
                    }
                }
            }
        }
    }
}
