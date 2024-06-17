using EpicJewels.Common;
using HarmonyLib;
using Jewelcrafting;
using Jotunn;

namespace EpicJewels.EffectHelpers
{

    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    public static class IncreaseDamageByPowers
    {
        private static void Prefix(HitData hit)
        {
            if (hit.GetAttacker() is Player attacker)
            {
                float original_total_dmg = hit.m_damage.m_blunt + hit.m_damage.m_pierce + hit.m_damage.m_slash + hit.m_damage.m_fire + hit.m_damage.m_lightning + hit.m_damage.m_frost + hit.m_damage.m_spirit + hit.m_damage.m_poison;
                float added_blunt_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("AddBluntDamage").Power / 100);
                float added_slash_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("AddSlashDamage").Power / 100);
                float added_pierce_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("AddPierceDamage").Power / 100);
                // float added_true_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddStaggerDamage.Config>("AddTrueDamage").Power / 100);
                float added_lightning_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("AddLightningDamage").Power / 100);
                float added_spirit_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("AddSpiritDamage").Power / 100);
                float added_pickaxe_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddPickaxeDamage.Config>("AddPickaxeDamage").Power / 100);
                float added_chop_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.AddPickaxeDamage.Config>("AddPickaxeDamage").Power / 100);
                float sum_of_added_dmg = added_blunt_dmg + added_slash_dmg + added_pierce_dmg + added_lightning_dmg + added_spirit_dmg + added_pickaxe_dmg;

                if (UnityEngine.Random.value < 0.2f && Player.m_localPlayer.GetEffectPower<GemEffects.Inferno.Config>("Inferno").Power > 0)
                {
                    float added_fire_dmg = original_total_dmg * (Player.m_localPlayer.GetEffectPower<GemEffects.Inferno.Config>("Inferno").Power / 100);
                    hit.m_damage.m_fire += added_fire_dmg;
                    if (Config.EnableDebugMode.Value) { Logger.LogInfo($"Inferno activated, added fire damage: {added_fire_dmg}"); }
                }
                
                if (Config.EnableDebugMode.Value)
                {
                    Logger.LogInfo($"Added Damage {sum_of_added_dmg} = blunt:{added_blunt_dmg} slash:{added_slash_dmg} pierce:{added_pierce_dmg} lightning: {added_lightning_dmg} spirit: {added_spirit_dmg} pickaxe: {added_pickaxe_dmg} original_total_dmg {original_total_dmg}");
                }
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
