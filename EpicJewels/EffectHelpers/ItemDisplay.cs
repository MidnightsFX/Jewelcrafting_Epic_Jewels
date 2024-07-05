using HarmonyLib;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using static EpicJewels.GemEffects.EffectList;
using static Skills;

namespace EpicJewels.EffectHelpers
{
    [HarmonyPatch(typeof(HitData.DamageTypes), nameof(HitData.DamageTypes.GetTooltipString), typeof(Skills.SkillType))]
    public static class ItemDisplay
    {
        private static void Postfix(HitData.DamageTypes __instance, Skills.SkillType skillType, ref String __result)
        {
            // Guard clause due to the postfixed method also having scenarios it can be called without the player defined
            if (Player.m_localPlayer == null) {  return; }

            // Will need to add a way to provide weapon information for damage types it currently doesn't have
            Player.m_localPlayer.GetSkills().GetRandomSkillRange(out var min, out var max, skillType);
            float total_dmg = __instance.GetTotalDamage();

            // Build a list of damage effects which the current player has active.
            // This allows us to add an entry to the damages listed for a weapon if one of the effects gives a bonus which the weapon does not already have
            //Dictionary<string, int> damage = new Dictionary<string, int>();
            //foreach (DmgEffect effect in Enum.GetValues(typeof(DmgEffect)))
            //{
            //    byte[] power_data = Player.m_localPlayer.m_nview.m_zdo.GetByteArray(effect.ToString().GetHashCode());
            //    EpicJewels.EJLog.LogDebug($"Effect {effect} effect data: |{power_data}|");
            //    if (power_data != null && power_data.Length > 0)
            //    {
            //        int dmg = BitConverter.ToInt32(power_data, 0);
            //        damage.Add(effect.ToString(), dmg);
            //    }
            //}
            Dictionary<String, DmgModDetails> player_damage_modifiers_active = DetermineCharacterDamageModifiers();

            List<String> entry_lines = __result.Split('\n').ToList();
            List<String> result_lines = new List<string> (entry_lines);
            for (int i = 0; i < entry_lines.Count; i++)
            {
                
                // Skip all the short lines, we don't care about them
                if (entry_lines[i].Length < 17) {  continue; }
                
                string line_desc = entry_lines[i].Split(':')[0].Trim();
                EpicJewels.EJLog.LogDebug($"ItemDescription {entry_lines[i]} |{line_desc}|");
                switch (line_desc)
                {
                    case "$inventory_damage":
                        break;
                    case "$inventory_blunt":
                        if (player_damage_modifiers_active.ContainsKey("AddBluntDamage"))
                        {
                            player_damage_modifiers_active["AddBluntDamage"].added = true;
                            result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, __instance.m_blunt, min, max, player_damage_modifiers_active["AddBluntDamage"].power); 
                        }
                        break;
                    case "$inventory_slash":
                        if (player_damage_modifiers_active.ContainsKey("AddSlashDamage"))
                        {
                            player_damage_modifiers_active["AddSlashDamage"].added = true;
                            result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, __instance.m_slash, min, max, player_damage_modifiers_active["AddSlashDamage"].power);
                        }
                        break;
                    case "$inventory_pierce":
                        if (player_damage_modifiers_active.ContainsKey("AddPierceDamage"))
                        {
                            player_damage_modifiers_active["AddPierceDamage"].added = true;
                            result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, __instance.m_pierce, min, max, player_damage_modifiers_active["AddPierceDamage"].power);
                        }
                        break;
                    case "$inventory_lightning":
                        if (player_damage_modifiers_active.ContainsKey("AddLightningDamage"))
                        {
                            player_damage_modifiers_active["AddLightningDamage"].added = true;
                            result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, __instance.m_lightning, min, max, player_damage_modifiers_active["AddLightningDamage"].power);
                        }
                        break;
                    case "$inventory_spirit":
                        if (player_damage_modifiers_active.ContainsKey("AddSpiritDamage"))
                        {   
                            player_damage_modifiers_active["AddSpiritDamage"].added = true;
                            result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, __instance.m_spirit, min, max, player_damage_modifiers_active["AddSpiritDamage"].power);
                        }
                        break;
                } 
            }

            // Add damage values that we are currently 
            foreach(var entry in player_damage_modifiers_active)
            {
                if (entry.Value.added == true) { continue; }
                
                result_lines.Add(BuildModifierExplainer(entry.Value.localizedName, total_dmg, min, max, entry.Value.power));
            }
            // Return our modified version of the item tooltip
            string result_modified = string.Join("\n", result_lines.ToArray());
            EpicJewels.EJLog.LogDebug($"ItemDescription {result_modified}");
            __result = result_modified;
        }

        class DmgModDetails
        {
            public string localizedName { get; set; }
            public bool added { get; set; }
            public float power { get; set; }
        }


        private static Dictionary<String, DmgModDetails> DetermineCharacterDamageModifiers()
        {
            Dictionary<String, DmgModDetails> set_damage_modifiers = new Dictionary<String, DmgModDetails>();
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("AddSpiritDamage").Power > 0)
            {
                set_damage_modifiers.Add("AddSpiritDamage", new DmgModDetails() { added = false, localizedName = "$inventory_spirit", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("AddSpiritDamage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("AddLightningDamage").Power > 0)
            {
                set_damage_modifiers.Add("AddLightningDamage", new DmgModDetails() { added = false, localizedName = "$inventory_lightning", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("AddLightningDamage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("AddPierceDamage").Power > 0)
            {
                set_damage_modifiers.Add("AddPierceDamage", new DmgModDetails() { added = false, localizedName = "$inventory_pierce", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("AddPierceDamage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("AddSlashDamage").Power > 0)
            {
                set_damage_modifiers.Add("AddSlashDamage", new DmgModDetails() { added = false, localizedName = "$inventory_slash", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("AddSlashDamage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("AddBluntDamage").Power > 0)
            {
                set_damage_modifiers.Add("AddBluntDamage", new DmgModDetails() { added = false, localizedName = "$inventory_blunt", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("AddBluntDamage").Power });
            }
            return set_damage_modifiers;
        }

        private static string AddModifierExplainer(string current_line, float total_dmg, float m_dmg_value, float min, float max, float bonus_power)
        {
            float bonus_dmg = total_dmg * (bonus_power / 100);
            string[] line_arr = current_line.Split(' ');
            EpicJewels.EJLog.LogDebug($"Modifying {line_arr[1]}");
            // Change the damage text color to the specified one
            float dmg = int.Parse(line_arr[1].Replace("<color=orange>", "").Replace("</color>", ""));
            line_arr[1] = $"<color=purple>{(dmg + bonus_dmg).ToString("F1")}</color>";
            // Not sure if this will be more confusing or not, maybe just recoloring is enough
            // Add the sum of bonus damage
            line_arr[1] += $" <color=purple>[+{bonus_dmg.ToString("F1")}]</color>";

            line_arr[2] = $"<color=purple>({Mathf.RoundToInt((m_dmg_value + bonus_dmg ) * min)}-{Mathf.RoundToInt((m_dmg_value + bonus_dmg) * max)})";
            EpicJewels.EJLog.LogDebug($"Result {line_arr[1]}");
            return string.Join(" ", line_arr);
        }

        private static string BuildModifierExplainer(string dmg_localization, float total_dmg, float min, float max, float bonus_power)
        {
            float bonus_dmg = total_dmg * (bonus_power / 100);
            return $"{dmg_localization}: <color=purple>{(bonus_dmg).ToString("F1")}</color> <color=purple>({Mathf.RoundToInt((bonus_dmg) * min)}-{Mathf.RoundToInt((bonus_dmg) * max)})</color>";
        }
    }
}
