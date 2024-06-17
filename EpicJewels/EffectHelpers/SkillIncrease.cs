using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Skills;

namespace EpicJewels.EffectHelpers
{
    [HarmonyPatch(typeof(Skills), nameof(Skills.GetSkillFactor))]
    public static class IncreaseHarvestingSkills
    {
        [UsedImplicitly]
        private static void Postfix(Skills __instance, SkillType skillType, ref float __result)
        {
            float skill_increase = 0f;
            switch (skillType)
            {
                case SkillType.Swords:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertSwordsman skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Spears:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertSpearmaiden skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Clubs:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertSmasher skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Polearms:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertPolearms skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.BloodMagic:
                case SkillType.ElementalMagic:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertMage skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Fishing:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertFisher skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Knives:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertDaggers skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Unarmed:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertBrawler skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Axes:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertAxemaster skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Blocking:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertShieldbearer skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Jump:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertAcrobat skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.WoodCutting:
                case SkillType.Pickaxes:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertHarvester skill increase {skill_increase}"); }
                    }
                    break;
                case SkillType.Run:
                    if (__instance.m_player.GetEffectPower<GemEffects.ExpertSprinter.Config>("ExpertSprinter").Power > 0)
                    {
                        skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSprinter.Config>("ExpertSprinter").Power;
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"ExpertSprinter skill increase {skill_increase}"); }
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
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertHarvester.Config>("ExpertHarvester").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSwordsman.Config>("ExpertSwordsman").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSpearmaiden.Config>("ExpertSpearmaiden").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertSmasher.Config>("ExpertSmasher").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertPolearms.Config>("ExpertPolearms").Power;
                    // mage is included twice, one for blood once for elemental
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertMage.Config>("ExpertMage").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertFisher.Config>("ExpertFisher").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertDaggers.Config>("ExpertDaggers").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertBrawler.Config>("ExpertBrawler").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertAxemaster.Config>("ExpertAxemaster").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertShieldbearer.Config>("ExpertShieldbearer").Power;
                    skill_increase += __instance.m_player.GetEffectPower<GemEffects.ExpertAcrobat.Config>("ExpertAcrobat").Power;
                    break;
            }
            
            float final_skill_increase = (skill_increase + 100) / 100;
            // if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"Final skill increase {final_skill_increase}%"); }
            __result *= final_skill_increase;
        }
    }
}
