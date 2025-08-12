using EpicJewels.Common;
using HarmonyLib;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace EpicJewels.EffectHelpers
{
    
    public static class ItemDisplay
    {
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetTooltip), typeof(ItemDrop.ItemData), typeof(int), typeof(bool), typeof(float), typeof(int))]
        public static class ItemToolTipDisplayEnhancer
        {
            private static void Postfix(ItemDrop.ItemData item, ref String __result)
            {
                if (Player.m_localPlayer == null || Player.m_localPlayer.IsItemEquiped(item) == false || Config.EnableItemTooltipDisplay.Value == false) { return; }

                Player.m_localPlayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                float total_dmg = item.m_shared.m_damages.GetTotalDamage();
                Dictionary<String, DmgModDetails> pdm_active = DetermineCharacterDamageModifiers();

                List<String> entry_lines = __result.Split('\n').ToList();
                List<String> result_lines = new List<string>(entry_lines);
                for (int i = 0; i < entry_lines.Count; i++)
                {
                    // Skip all the short lines, we don't care about them
                    if (entry_lines[i].Length < 17) { continue; }

                    string line_desc = entry_lines[i].Split(':')[0].Trim();
                    EpicJewels.EJLog.LogDebug($"ItemDescription {entry_lines[i]} | {line_desc}|");
                    switch (line_desc)
                    {
                        case "$inventory_damage":
                            break;
                        case "$inventory_fire":
                            if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_blunt, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_poison":
                            if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_blunt, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_frost":
                            if (pdm_active.ContainsKey("Coin Hoarder"))
                            {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_blunt, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_blunt":
                            if (pdm_active.ContainsKey("Add Blunt Damage"))
                            {
                                pdm_active["Add Blunt Damage"].added = true;
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_blunt, min, max, pdm_active["Add Blunt Damage"].power, pdm_active["Coin Hoarder"].power);
                            } else if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_blunt, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_slash":
                            if (pdm_active.ContainsKey("Add Slash Damage"))
                            {
                                pdm_active["Add Slash Damage"].added = true;
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_slash, min, max, pdm_active["Add Slash Damage"].power, pdm_active["Coin Hoarder"].power);
                            } else if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_slash, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_pierce":
                            if (pdm_active.ContainsKey("Add Pierce Damage"))
                            {
                                pdm_active["Add Pierce Damage"].added = true;
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_pierce, min, max, pdm_active["Add Pierce Damage"].power, pdm_active["Coin Hoarder"].power);
                            } else if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_pierce, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_lightning":
                            if (pdm_active.ContainsKey("Add Lightning Damage"))
                            {
                                pdm_active["Add Lightning Damage"].added = true;
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_lightning, min, max, pdm_active["Add Lightning Damage"].power, pdm_active["Coin Hoarder"].power);
                            } else if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_lightning, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                        case "$inventory_spirit":
                            if (pdm_active.ContainsKey("Add Spirit Damage"))
                            {
                                pdm_active["Add Spirit Damage"].added = true;
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_spirit, min, max, pdm_active["Add Spirit Damage"].power, pdm_active["Coin Hoarder"].power);
                            } else if (pdm_active.ContainsKey("Coin Hoarder")) {
                                result_lines[i] = AddModifierExplainer(entry_lines[i], total_dmg, item.m_shared.m_damages.m_spirit, min, max, 0f, pdm_active["Coin Hoarder"].power);
                            }
                            break;
                    }
                }

                // Add damage values that we are currently 
                foreach (var entry in pdm_active)
                {
                    if (entry.Value.added == true || entry.Key == "Coin Hoarder") { continue; }

                    result_lines.Add(BuildModifierExplainer(entry.Value.localizedName, total_dmg, min, max, entry.Value.power));
                }
                // Return our modified version of the item tooltip
                string result_modified = string.Join("\n", result_lines.ToArray());
                //EpicJewels.EJLog.LogDebug($"ItemDescription {result_modified}");
                __result = result_modified;
            }
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
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("Add Spirit Damage").Power > 0)
            {
                set_damage_modifiers.Add("Add Spirit Damage", new DmgModDetails() { added = false, localizedName = "$inventory_spirit", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddSpiritDamage.Config>("Add Spirit Damage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("Add Lightning Damage").Power > 0)
            {
                set_damage_modifiers.Add("Add Lightning Damage", new DmgModDetails() { added = false, localizedName = "$inventory_lightning", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddLightningDamage.Config>("Add Lightning Damage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("Add Pierce Damage").Power > 0)
            {
                set_damage_modifiers.Add("Add Pierce Damage", new DmgModDetails() { added = false, localizedName = "$inventory_pierce", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddPierceDamage.Config>("Add Pierce Damage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("Add Slash Damage").Power > 0)
            {
                set_damage_modifiers.Add("Add Slash Damage", new DmgModDetails() { added = false, localizedName = "$inventory_slash", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddSlashDamage.Config>("Add Slash Damage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("Add Blunt Damage").Power > 0)
            {
                set_damage_modifiers.Add("Add Blunt Damage", new DmgModDetails() { added = false, localizedName = "$inventory_blunt", power = Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("Add Blunt Damage").Power });
            }
            if (Player.m_localPlayer.GetEffectPower<GemEffects.AddBluntDamage.Config>("Coin Hoarder").Power > 0)
            {
                set_damage_modifiers.Add("Coin Hoarder", new DmgModDetails() { added = false, power = GemEffects.CoinHoarder.CoinHoarderBonusCalc(Player.m_localPlayer) });
            }
            return set_damage_modifiers;
        }

        private static string AddModifierExplainer(string current_line, float total_dmg, float m_dmg_value, float min, float max, float bonus_power, float coinhoarder_mult = 1f)
        {
            float bonus_dmg = total_dmg * (bonus_power / 100);
            string[] line_arr = current_line.Split(' ');
            //EpicJewels.EJLog.LogInfo($"Modifying ({current_line}) {line_arr[1]}");
            // Change the damage text color to the specified one
            Match match = Regex.Match(line_arr[1], @"(?<=>)(\d+)(?=<)");
            if (match.Success == false) {
                return current_line;
            }
            // EpicJewels.EJLog.LogDebug($"Match {match.Value}");

            int dmg;
            bool parsed = int.TryParse(match.Value, out dmg);
            if (parsed == false) {
                return current_line; 
            }
            line_arr[1] = $"<color=purple>{((dmg + bonus_dmg) * coinhoarder_mult).ToString("F1")}</color>";
            // Not sure if this will be more confusing or not, maybe just recoloring is enough
            // Add the sum of bonus damage
            line_arr[1] += $" <color=purple>[+{(bonus_dmg + (coinhoarder_mult * dmg - dmg)).ToString("F1")}]</color>";

            line_arr[2] = $"<color=purple>({Mathf.RoundToInt((m_dmg_value + bonus_dmg + (coinhoarder_mult * dmg - dmg)) * min)}-{Mathf.RoundToInt((m_dmg_value + bonus_dmg + (coinhoarder_mult * dmg - dmg)) * max)})";
            //EpicJewels.EJLog.LogDebug($"Result {line_arr[1]}");
            return string.Join(" ", line_arr);
        }

        private static string BuildModifierExplainer(string dmg_localization, float total_dmg, float min, float max, float bonus_power)
        {
            float bonus_dmg = total_dmg * (bonus_power / 100);
            return $"{dmg_localization}: <color=purple>{(bonus_dmg).ToString("F1")}</color> <color=purple>({Mathf.RoundToInt((bonus_dmg) * min)}-{Mathf.RoundToInt((bonus_dmg) * max)})</color>";
        }
    }
}
