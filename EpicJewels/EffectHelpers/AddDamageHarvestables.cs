using HarmonyLib;
using Jewelcrafting;

namespace EpicJewels.EffectHelpers
{
    public static class AddDamageHarvestables
    {
        private static HitData ModifyHarvestDamage(HitData hit)
        {
            if (hit.GetAttacker() is Player)
            {
                Player player = hit.GetAttacker() as Player;
                float original_total_dmg = hit.m_damage.GetTotalDamage();
                float added_pickaxe_dmg = 0;
                if (player.GetEffectPower<GemEffects.AddPickaxeDamage.Config>("Add Pickaxe Damage").Chance >= UnityEngine.Random.value) {
                    added_pickaxe_dmg = original_total_dmg * (player.GetEffectPower<GemEffects.AddPickaxeDamage.Config>("Add Pickaxe Damage").Power / 100);
                }
                float added_chop_dmg = 0;
                if (player.GetEffectPower<GemEffects.AddChopDamage.Config>("Add Chop Damage").Chance >= UnityEngine.Random.value) {
                    added_chop_dmg = original_total_dmg * (player.GetEffectPower<GemEffects.AddChopDamage.Config>("Add Chop Damage").Power / 100);
                }

                if (player.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Power > 0 || player.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power > 0)
                {
                    float eitr_cost = player.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Cost;
                    if (player.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power > 0) { eitr_cost += 5; }
                    if (player.HaveEitr(eitr_cost))
                    {
                        float eitr_fused_powerup = ((player.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Power + player.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power) / 100f);
                        // EpicJewels.EJLog.LogDebug($"Eitr powered attack powered up multiplier {eitr_fused_powerup} cost {eitr_cost}");
                        added_chop_dmg += hit.m_damage.m_chop * eitr_fused_powerup;
                        added_pickaxe_dmg += hit.m_damage.m_pickaxe * eitr_fused_powerup;
                        player.UseEitr(eitr_cost);
                    }
                    else
                    {
                        EpicJewels.EJLog.LogDebug($"Eitr powered attack not triggered due to cost {eitr_cost}");
                    }
                }
                float sum_of_added_dmg = added_chop_dmg + added_pickaxe_dmg;
                EpicJewels.EJLog.LogDebug($"Added Damage {sum_of_added_dmg} = pickaxe: {added_pickaxe_dmg} chop: {added_chop_dmg} original_total_dmg {original_total_dmg}");
                hit.m_damage.m_chop += added_chop_dmg;
                hit.m_damage.m_pickaxe += added_pickaxe_dmg;
            }
            return hit;
        }

        [HarmonyPatch(typeof(MineRock), nameof(MineRock.RPC_Hit))]
        private static class DamageRock
        {
            private static void Prefix(HitData hit)
            {
                ModifyHarvestDamage(hit);
            }
        }

        [HarmonyPatch(typeof(MineRock5), nameof(MineRock5.RPC_Damage))]
        private static class DamageRock5
        {
            private static void Prefix(HitData hit)
            {
                ModifyHarvestDamage(hit);
            }
        }

        [HarmonyPatch(typeof(TreeBase), nameof(TreeBase.RPC_Damage))]
        private static class DamageTreebase
        {
            private static void Prefix(HitData hit)
            {
                ModifyHarvestDamage(hit);
            }
        }

        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.RPC_Damage))]
        private static class DamageTreeLog
        {
            private static void Prefix(HitData hit)
            {
                ModifyHarvestDamage(hit);
            }
        }

        [HarmonyPatch(typeof(Destructible), nameof(Destructible.RPC_Damage))]
        private static class DamageDestructible
        {
            private static void Prefix(HitData hit)
            {
                ModifyHarvestDamage(hit);
            }
        }
    }
}
