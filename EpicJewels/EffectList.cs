using Jewelcrafting;
using System.Collections.Generic;

namespace EpicJewels.GemEffects
{
    public static class EffectList
    {
        // Firestarter,
        // Iceheart,
        // Snakebite,
        public enum DmgEffect
        {
            AddBluntDamage,
            AddPierceDamage,
            AddSlashDamage,
            AddSpiritDamage,
            AddLightningDamage,
            AddPickaxeDamage,
            AddChopDamage
        }
        
        // public static IReadOnlyList<T> GetDmgValues<T>() { return (T[])DmgEffect.GetValues(typeof(T)); }

        public static void AddGemEffects()
        {
            API.AddGemEffect<BluntResistance.Config>("BluntResistance", "$EJ_blunt_resistance_header.", "$EJ_blunt_resistance_header $EJ_by $1%.");
            API.AddGemEffect<PierceResistance.Config>("PierceResistance", "$EJ_pierce_resistance_header.", "$EJ_pierce_resistance_header $EJ_by $1%.");
            API.AddGemEffect<SlashResistance.Config>("SlashResistance", "$EJ_slash_resistance_header.", "$EJ_slash_resistance_header $EJ_by $1%.");
            API.AddGemEffect<FireResistance.Config>("FireResistance", "$EJ_fire_resistance_header.", "$EJ_fire_resistance_header $EJ_by $1%.");
            API.AddGemEffect<PoisonResistance.Config>("PoisonResistance", "$EJ_poison_resistance_header.", "$EJ_poison_resistance_header $EJ_by $1%.");
            API.AddGemEffect<LightningResistance.Config>("LightningResistance", "$EJ_lightning_resistance_header.", "$EJ_lightning_resistance_header $EJ_by $1%.");
            API.AddGemEffect<AddBluntDamage.Config>("AddBluntDamage", "$EJ_blunt_dmg_header.", "$EJ_blunt_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddPierceDamage.Config>("AddPierceDamage", "$EJ_pierce_dmg_header.", "$EJ_pierce_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddSlashDamage.Config>("AddSlashDamage", "$EJ_slash_dmg_header.", "$EJ_slash_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddSpiritDamage.Config>("AddSpiritDamage", "$EJ_spirit_dmg_header.", "$EJ_spirit_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddLightningDamage.Config>("AddLightningDamage", "$EJ_lightning_dmg_header.", "$EJ_lightning_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddPickaxeDamage.Config>("AddPickaxeDamage", "$EJ_pickaxe_dmg_header.", "$EJ_pickaxe_dmg_header $EJ_eq $1% $EJ_dmg_explained_end");
            API.AddGemEffect<AddChopDamage.Config>("AddChopDamage", "$EJ_woodcutting_dmg_header.", "$EJ_woodcutting_dmg_header $EJ_eq $1% $EJ_dmg_explained_end");
            API.AddGemEffect<Inferno.Config>("Inferno", "$EJ_inferno_header", "$2% $EJ_inferno_ep1 $1% $EJ_inferno_ep2");
            API.AddGemEffect<IncreaseEitr.Config>("IncreaseEitr", "$EJ_more_eitr_header", "$EJ_more_eitr_details $1.");
            API.AddGemEffect<IncreaseStamina.Config>("IncreaseStamina", "$EJ_more_stamina_header", "$EJ_more_stamina_explained $1.");
            API.AddGemEffect<IncreaseStaminaRegen.Config>("IncreaseStaminaRegen", "$EJ_stamina_regen_header", "$EJ_stamina_regen_explained $1%.");
            API.AddGemEffect<BlockReduceStamina.Config>("BlockReduceStamina", "$EJ_stamina_block_cost_header", "$EJ_stamina_block_cost_explained $1%.");
            API.AddGemEffect<WeaponReducedStamina.Config>("WeaponReducedStamina", "$EJ_stamina_weapon_cost_header", "$EJ_stamina_weapon_cost_explained $1%.");
            API.AddGemEffect<CoinGreed.Config>("CoinGreed", "$EJ_coingreed_header", "$EJ_coingreed_pt1 $2% $EJ_coingreed_pt2 1-$1 $EJ_coingreed_pt3");
            API.AddGemEffect<CoinHoarder.Config>("CoinHoarder", "$EJ_coinhoarder", "$EJ_coinhoarder_pt1 $1% $EJ_coinhoarder_pt2");
            API.AddGemEffect<WaterResistant.Config>("WaterResistant", "$EJ_water_prevent", "$EJ_water_prevent_pt1 $1 $EJ_water_prevent_pt2");
            API.AddGemEffect<WaterFrenzy.Config>("WaterFrenzy", "$EJ_water_dmg_buff", "$EJ_dmg_buff_pt1 $1% $EJ_water_dmg_buff_pt2");
            API.AddGemEffect<WaterSwiftness.Config>("WaterSwiftness", "$EJ_water_speed_buff", "$EJ_speed_buff_pt1 $1% $EJ_water_speed_buff_pt2");
            API.AddGemEffect<BurningViking.Config>("BurningViking", "$EJ_fire_speed_buff", "$EJ_speed_buff_pt1 $1% $EJ_fire_speed_buff_pt2");
            API.AddGemEffect<BurningFrenzy.Config>("BurningFrenzy", "$EJ_fire_dmg_buff", "$EJ_dmg_buff_pt1 $1% $EJ_fire_dmg_buff_pt2");
            API.AddGemEffect<ExpertFisher.Config>("ExpertFisher", "$EJ_skill_fishing", "$EJ_skill_fishing_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertMage.Config>("ExpertMage", "$EJ_skill_magic", "$EJ_skill_magic_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertHarvester.Config>("ExpertHarvester", "$EJ_skill_harvest", "$EJ_skill_harvest_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertBrawler.Config>("ExpertBrawler", "$EJ_brawling", "$EJ_brawling_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertAcrobat.Config>("ExpertAcrobat", "$EJ_jump", "$EJ_jump_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertDaggers.Config>("ExpertDaggers", "$EJ_knives", "$EJ_knives_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSwordsman.Config>("ExpertSwordsman", "$EJ_swords", "$EJ_swords_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSmasher.Config>("ExpertSmasher", "$EJ_maces", "$EJ_maces_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertPolearms.Config>("ExpertPolearms", "$EJ_polearms", "$EJ_polearms_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSpearmaiden.Config>("ExpertSpearmaiden", "$EJ_spears", "$EJ_spears_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertAxemaster.Config>("ExpertAxemaster", "$EJ_axes", "$EJ_axes_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSprinter.Config>("ExpertSprinter", "$EJ_sprinter", "$EJ_sprinter_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ReduceWeight.Config>("ReduceWeight", "$EJ_weight_reduce", "$EJ_weight_reduce_pt1 $1% $EJ_weight_reduce_pt2");
            API.AddGemEffect<CoverOfDarkness.Config>("CoverOfDarkness", "$EJ_cover_darkness", "$2% $EJ_cover_darkness_pt1 $1% $EJ_cover_darkness_pt2");
            API.AddGemEffect<EitrConversion.Config>("EitrConversion", "$EJ_eitr_conversion", "$2% $EJ_eitr_conversion_pt1 $1% $EJ_eitr_conversion_pt2");
            API.AddGemEffect<Retribution.Config>("Retribution", "$EJ_retribution", "$2% $EJ_retribution_pt1 $1% $EJ_retribution_pt2");

            API.AddGemEffect<CombatSpirit.Config>("CombatSpirit", "A spirit helps you in combat.", "A spirit aids you in combat for $1 seconds. Returns after a cooldown.");
            API.AddGemEffect<IntenseFire.Config>("IntenseFire", "An affinity for fire.", "You are +$1% fire resistant and have a higher chance to trigger Inferno.");
            API.AddGemEffect<SlipperyWhenWet.Config>("SlipperyWhenWet", "Water quickens you.", "You are $1% faster when wet.");
            API.AddGemEffect<WeaponMaster.Config>("WeaponMaster", "Experianced with weapons.", "Your skill with all weapons is $1% higher.");
        }
    }
}
