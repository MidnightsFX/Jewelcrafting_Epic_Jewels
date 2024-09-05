using HarmonyLib;
using Jewelcrafting;
using UnityEngine;

namespace EpicJewels.EffectHelpers
{

    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class IncreaseDamageByPowers
    {
        private static void Prefix(HitData hit)
        {
            if (hit.GetAttacker() is Player)
            {
                float original_total_dmg = hit.m_damage.GetTotalDamage();
                float added_blunt_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("Add Blunt Damage").Power / 100);
                float added_slash_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("Add Slash Damage").Power / 100);
                float added_pierce_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("Add Pierce Damage").Power / 100);
                // float added_true_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddStaggerDamage.Config>("AddTrueDamage").Power / 100);
                float added_lightning_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("Add Lightning Damage").Power / 100);
                float added_spirit_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("Add Spirit Damage").Power / 100);
                float added_pickaxe_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddPickaxeDamage.Config>("Add Pickaxe Damage").Power / 100);
                float added_chop_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddChopDamage.Config>("Add Chop Damage").Power / 100);


                if (Player.m_localPlayer.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Power > 0 || Player.m_localPlayer.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power > 0)
                {
                    float eitr_cost = Player.m_localPlayer.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Cost;
                    if (Player.m_localPlayer.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power > 0) { eitr_cost += 5; }
                    if (Player.m_localPlayer.HaveEitr(eitr_cost))
                    {
                        float eitr_fused_powerup = ((Player.m_localPlayer.GetEffectPower<GemEffects.EitrFused.Config>("Eitr Fused").Power + Player.m_localPlayer.GetEffectPower<GemEffects.Spellsword.Config>("Spellsword").Power) / 100f) + 1f;
                        EpicJewels.EJLog.LogDebug($"Eitr powered attack powered up multiplier {eitr_fused_powerup} cost {eitr_cost}");
                        added_chop_dmg += hit.m_damage.m_chop * eitr_fused_powerup;
                        added_pickaxe_dmg += hit.m_damage.m_pickaxe * eitr_fused_powerup;
                        added_spirit_dmg += hit.m_damage.m_spirit * eitr_fused_powerup;
                        added_lightning_dmg += hit.m_damage.m_lightning * eitr_fused_powerup;
                        added_pierce_dmg += hit.m_damage.m_pierce * eitr_fused_powerup;
                        added_slash_dmg += hit.m_damage.m_slash * eitr_fused_powerup;
                        added_blunt_dmg += hit.m_damage.m_blunt * eitr_fused_powerup;
                        Player.m_localPlayer.UseEitr(eitr_cost);
                    } else
                    {
                        EpicJewels.EJLog.LogDebug($"Eitr powered attack not triggered due to cost {eitr_cost}");
                    }
                }

                float sum_of_added_dmg = added_blunt_dmg + added_slash_dmg + added_pierce_dmg + added_lightning_dmg + added_spirit_dmg + added_pickaxe_dmg + added_chop_dmg;

                float inferno_chance = Player.m_localPlayer.GetEffectPower<GemEffects.Inferno.Config>("Inferno").Chance;
                // Bonus from intenseFire
                if (Player.m_localPlayer.GetEffectPower<GemEffects.IntenseFire.Config>("Intense Fire").Power > 0) { inferno_chance = inferno_chance * 1.5f; }

                if (Player.m_localPlayer.GetEffectPower<GemEffects.Inferno.Config>("Inferno").Power > 0 && Random.value < inferno_chance)
                {
                    float added_fire_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.Inferno.Config>("Inferno").Power / 100);
                    hit.m_damage.m_fire += added_fire_dmg;
                    EpicJewels.EJLog.LogDebug($"Inferno activated, added fire damage: {added_fire_dmg}");
                }
                
                EpicJewels.EJLog.LogDebug($"Added Damage {sum_of_added_dmg} = blunt:{added_blunt_dmg} slash:{added_slash_dmg} pierce:{added_pierce_dmg} lightning: {added_lightning_dmg} spirit: {added_spirit_dmg} pickaxe: {added_pickaxe_dmg} original_total_dmg {original_total_dmg}");
                // hit.m_damage.m_damage += added_true_dmg;
                // hit.m_damage.m_damage += sum_of_added_dmg;
                hit.m_damage.m_blunt += added_blunt_dmg;
                hit.m_damage.m_pierce += added_pierce_dmg;
                hit.m_damage.m_slash += added_slash_dmg;
                // Fire, frost, poison are already implemented by jewelcrafting
                //hit.m_damage.m_fire += added_fire_dmg;
                //hit.m_damage.m_frost += damage_multiplier;
                //hit.m_damage.m_poison += damage_multiplier;
                hit.m_damage.m_lightning += added_lightning_dmg;
                hit.m_damage.m_spirit += added_spirit_dmg;
                hit.m_damage.m_chop += added_chop_dmg;
                hit.m_damage.m_pickaxe += added_pickaxe_dmg;
            }
        }
    }
}
