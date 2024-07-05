using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Skills;

namespace EpicJewels.EffectHelpers
{

    [HarmonyPatch(typeof(Skills), nameof(Skills.GetSkillFactor))]
    public static class IncreaseSkillPatch
    {
        [UsedImplicitly]
        private static void Postfix(Skills __instance, SkillType skillType, ref float __result)
        {
            __result = DetermineSkillIncrease(__instance.m_player, skillType, __result);
        }

        private static float DetermineSkillIncrease(Player player, SkillType selectedSkill, float current_skill_level)
        {
            float skill_increase = 0f;
            switch (selectedSkill)
            {
                case SkillType.Swords:
                    if (player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertSwordsman skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Spears:
                    if (player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertSpearmaiden skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Clubs:
                    if (player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertSmasher skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Polearms:
                    if (player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertPolearms skill increase {skill_increase}");
                    }
                    break;
                case SkillType.BloodMagic:
                case SkillType.ElementalMagic:
                    if (player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertMage skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Fishing:
                    if (player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertFisher skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Knives:
                    if (player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertDaggers skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Unarmed:
                    if (player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertBrawler skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Axes:
                    if (player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertAxemaster skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Blocking:
                    if (player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertShieldbearer skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Jump:
                    if (player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertAcrobat skill increase {skill_increase}");
                    }
                    break;
                case SkillType.WoodCutting:
                case SkillType.Pickaxes:
                    if (player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertHarvester skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Run:
                    if (player.GetEffectPower<GemEffects.ExpertSprinter.Config>("ExpertSprinter").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSprinter.Config>("ExpertSprinter").Power;
                        EpicJewels.EJLog.LogDebug($"ExpertSprinter skill increase {skill_increase}");
                    }
                    break;
                // These empty ones are already provided by base jewelcrafting
                case SkillType.None:
                case SkillType.Crossbows:
                case SkillType.Bows:
                case SkillType.Sneak:
                case SkillType.Swim:
                case SkillType.Ride:
                    break;
                case SkillType.All:
                    // harvester is included twice once for pickaxe, once for woodcutting
                    skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power;
                    // mage is included twice, one for blood once for elemental
                    skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power;
                    break;
            }

            float final_skill_increase = (skill_increase + 100) / 100;
            EpicJewels.EJLog.LogDebug($"Total skill increase {final_skill_increase}");
            return (current_skill_level * final_skill_increase);
        }

        // Modified from https://github.com/OrianaVenture/Randy_Vapok_ValheimMods/blob/main/EpicLoot/src/Magic/MagicItemEffects/AddSkillLevel.cs
        [HarmonyPatch(typeof(SkillsDialog), nameof(SkillsDialog.Setup))]
        public static class DisplayExtraSkillLevels_SkillsDialog_Setup_Patch
        {
            [UsedImplicitly]
            private static void Postfix(SkillsDialog __instance, Player player)
            {
                var allSkills = player.m_skills.GetSkillList();
                var elementList = new List<GameObject>();
                elementList = __instance.m_elements;

                foreach (var element in elementList)
                {
                    var tooltipComponent = element.GetComponentInChildren<UITooltip>();
                    var skill = allSkills.Find(s => s.m_info.m_description == tooltipComponent.m_text);

                    if (skill == null)
                    {
                        continue;
                    }
                    float base_skill_level = player.m_skills.GetSkill(skill.m_info.m_skill).m_level;
                    float modified_skill = DetermineSkillIncrease(player, skill.m_info.m_skill, base_skill_level);

                    if (modified_skill > base_skill_level)
                    {
                        float skill_increase = modified_skill - base_skill_level;
                        var levelbar = Utils.FindChild(element.transform, "bar");
                        var extraLevelbar = Utils.FindChild(element.transform, "extrabar")?.gameObject;

                        if (extraLevelbar == null)
                        {
                            extraLevelbar = Object.Instantiate(levelbar.gameObject, levelbar.parent);
                            extraLevelbar.transform.SetSiblingIndex(levelbar.GetSiblingIndex());
                            extraLevelbar.name = "extrabar";
                        }

                        extraLevelbar.SetActive(true);
                        var rect = extraLevelbar.GetComponent<RectTransform>();
                        rect.sizeDelta = new Vector2((skill.m_level + skill_increase) * 1.6f, rect.sizeDelta.y);
                        extraLevelbar.GetComponent<Image>().color = Color.magenta;
                        Utils.FindChild(element.transform, "leveltext").GetComponent<TMP_Text>().text += $"  <color=#{ColorUtility.ToHtmlStringRGBA(Color.magenta)}>+{skill_increase:#0.0}</color>";
                    }
                    else
                    {
                        var extralevelbar = Utils.FindChild(element.transform, "extrabar");
                        if (extralevelbar != null)
                        {
                            extralevelbar.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
