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

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class FreezingBlock_Patch
        {
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
                        frost_rebuke_hit.m_damage.m_frost = (player.GetEffectPower<Config>("Freezing Guard").Power / 100) * hit.m_damage.GetTotalDamage();
                        attacker.ApplyDamage(frost_rebuke_hit, true, true);
                        attacker.m_seman.AddStatusEffect(froststatus, true, 3, player.m_skills.GetSkill(Skills.SkillType.Blocking).m_level);
                    }
                }
            }
        }
    }
}
