using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class FlamingGuard
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [AdditivePowerAttribute] public float Chance;
        }

        [HarmonyPriority(Priority.High)]
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class FlamingBlock_Patch
        {
            static HitData originalHit = null;
            private static void Prefix(HitData hit)
            {
                originalHit = new HitData(damage: hit.GetTotalDamage());
            }

            private static int burningstatus = "Burning".GetStableHashCode();
            private static void Postfix(Humanoid __instance, HitData hit, Character attacker, ref bool __result)
            {
                if (__instance is Player player && player.GetEffectPower<Config>("Burning Guard").Chance > 0 && __result == true && attacker.IsDead() == false)
                {
                    float roll = UnityEngine.Random.value;
                    float chance_max = (player.GetEffectPower<Config>("Burning Guard").Chance / 100);
                    // EpicJewels.EJLog.LogDebug($"Burning Guard chance roll: {roll} < {chance_max}");
                    if (roll < chance_max)
                    {
                        HitData flaming_rebuke_hit = new HitData();
                        flaming_rebuke_hit.m_damage.m_fire = (player.GetEffectPower<Config>("Burning Guard").Power / 100) * originalHit.m_damage.GetTotalDamage();
                        // EpicJewels.EJLog.LogDebug($"Hit dmg {originalHit} FlamingGuard returning damage {flaming_rebuke_hit.m_damage.m_fire}");
                        attacker.Damage(flaming_rebuke_hit);
                        attacker.m_seman.AddStatusEffect(burningstatus, true, 1, player.m_skills.GetSkill(Skills.SkillType.Blocking).m_level);
                    }
                }
            }
        }
    }
}
