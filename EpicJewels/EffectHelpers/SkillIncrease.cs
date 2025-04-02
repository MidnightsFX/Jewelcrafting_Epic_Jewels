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
                    if (player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("Expert Swordsman").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("Expert Swordsman").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Swords skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Spears:
                    if (player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("Expert Spearmaiden").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("Expert Spearmaiden").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Spears skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Clubs:
                    if (player.GetEffectPower<GemEffects.ExpertSmasher.Config>("Expert Smasher").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSmasher.Config>("Expert Smasher").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Clubs skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Polearms:
                    if (player.GetEffectPower<GemEffects.ExpertPolearms.Config>("Expert Polearms").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertPolearms.Config>("Expert Polearms").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Polearms skill increase {skill_increase}");
                    }
                    break;
                case SkillType.BloodMagic:
                case SkillType.ElementalMagic:
                    if (player.GetEffectPower<GemEffects.ExpertMage.Config>("Expert Mage").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("Expert Mage").Power;
                        //EpicJewels.EJLog.LogDebug($"Magic skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Fishing:
                    if (player.GetEffectPower<GemEffects.ExpertFisher.Config>("Expert Fisher").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertFisher.Config>("Expert Fisher").Power;
                        //EpicJewels.EJLog.LogDebug($"Fishing skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Knives:
                    if (player.GetEffectPower<GemEffects.ExpertDaggers.Config>("Expert Daggers").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertDaggers.Config>("Expert Daggers").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Knives skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Unarmed:
                    if (player.GetEffectPower<GemEffects.ExpertBrawler.Config>("Expert Brawler").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertBrawler.Config>("Expert Brawler").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Unarmed skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Axes:
                    if (player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("Expert Axemaster").Power > 0 || player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("Expert Axemaster").Power;
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Axes skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Jump:
                    if (player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("Expert Acrobat").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("Expert Acrobat").Power;
                        //EpicJewels.EJLog.LogDebug($"Jump skill increase {skill_increase}");
                    }
                    break;
                case SkillType.WoodCutting:
                case SkillType.Pickaxes:
                    if (player.GetEffectPower<GemEffects.ExpertHarvester.Config>("Expert Harvester").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("Expert Harvester").Power;
                        //EpicJewels.EJLog.LogDebug($"Pickaxe & woodcutting skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Run:
                    if (player.GetEffectPower<GemEffects.ExpertSprinter.Config>("Expert Sprinter").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.ExpertSprinter.Config>("Expert Sprinter").Power;
                        //EpicJewels.EJLog.LogDebug($"Run skill increase {skill_increase}");
                    }
                    break;
                case SkillType.Crossbows:
                    if (player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power > 0)
                    {
                        skill_increase += player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                        //EpicJewels.EJLog.LogDebug($"Crossbow skill increase {skill_increase}");
                    }
                    break;
                // These empty ones are already provided by base jewelcrafting
                case SkillType.Blocking:
                case SkillType.None:

                case SkillType.Bows:
                case SkillType.Sneak:
                case SkillType.Swim:
                case SkillType.Ride:
                    break;
                case SkillType.All:
                    float wm_bonus = player.GetEffectPower<GemEffects.WeaponMaster.Config>("Weapon Master").Power;
                    // harvester is included twice once for pickaxe, once for woodcutting
                    skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("Expert Harvester").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertHarvester.Config>("Expert Harvester").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("Expert Swordsman").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("Expert Spearmaiden").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertSmasher.Config>("Expert Smasher").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertPolearms.Config>("Expert Polearms").Power;
                    // mage is included twice, one for blood once for elemental
                    skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("Expert Mage").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertMage.Config>("Expert Mage").Power;
                    skill_increase += player.GetEffectPower<GemEffects.ExpertFisher.Config>("Expert Fisher").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertDaggers.Config>("Expert Daggers").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertBrawler.Config>("Expert Brawler").Power;
                    skill_increase += wm_bonus + player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("Expert Axemaster").Power;
                    // skill_increase += wm_bonus // Shield bonus
                    skill_increase += player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("Expert Acrobat").Power;

                    // EpicJewels.EJLog.LogDebug($"All skills increase {skill_increase}");
                    break;
            }

            EpicJewels.EJLog.LogDebug($"Total skill increase {skill_increase}");
            return (current_skill_level + skill_increase);
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
