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
            API.AddGemEffect<BluntResistance.Config>("Blunt Resistance", "$EJ_blunt_resistance_header.", "$EJ_blunt_resistance_header $EJ_by $1%.");
            API.AddGemEffect<PierceResistance.Config>("Pierce Resistance", "$EJ_pierce_resistance_header.", "$EJ_pierce_resistance_header $EJ_by $1%.");
            API.AddGemEffect<SlashResistance.Config>("Slash Resistance", "$EJ_slash_resistance_header.", "$EJ_slash_resistance_header $EJ_by $1%.");
            API.AddGemEffect<FireResistance.Config>("Fire Resistance", "$EJ_fire_resistance_header.", "$EJ_fire_resistance_header $EJ_by $1%.");
            API.AddGemEffect<PoisonResistance.Config>("Poison Resistance", "$EJ_poison_resistance_header.", "$EJ_poison_resistance_header $EJ_by $1%.");
            API.AddGemEffect<LightningResistance.Config>("Lightning Resistance", "$EJ_lightning_resistance_header.", "$EJ_lightning_resistance_header $EJ_by $1%.");
            API.AddGemEffect<AddBluntDamage.Config>("Add Blunt Damage", "$EJ_blunt_dmg_header.", "$EJ_blunt_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddPierceDamage.Config>("Add Pierce Damage", "$EJ_pierce_dmg_header.", "$EJ_pierce_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddSlashDamage.Config>("Add Slash Damage", "$EJ_slash_dmg_header.", "$EJ_slash_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddSpiritDamage.Config>("Add Spirit Damage", "$EJ_spirit_dmg_header.", "$EJ_spirit_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddLightningDamage.Config>("Add Lightning Damage", "$EJ_lightning_dmg_header.", "$EJ_lightning_dmg_header $EJ_eq $1% $EJ_dmg_explained_end $EJ_dmg_tooltip");
            API.AddGemEffect<AddPickaxeDamage.Config>("Add Pickaxe Damage", "$EJ_pickaxe_dmg_header.", "$EJ_pickaxe_dmg_header $EJ_eq $1% $EJ_dmg_explained_end");
            API.AddGemEffect<AddChopDamage.Config>("Add Chop Damage", "$EJ_woodcutting_dmg_header.", "$EJ_woodcutting_dmg_header $EJ_eq $1% $EJ_dmg_explained_end");
            API.AddGemEffect<Inferno.Config>("Inferno", "$EJ_inferno_header", "$2% $EJ_inferno_ep1 $1% $EJ_inferno_ep2");
            API.AddGemEffect<IncreaseEitr.Config>("Increase Eitr", "$EJ_more_eitr_header", "$EJ_more_eitr_details $1.");
            API.AddGemEffect<IncreaseStamina.Config>("Increase Stamina", "$EJ_more_stamina_header", "$EJ_more_stamina_explained $1.");
            API.AddGemEffect<IncreaseStaminaRegen.Config>("Increase Stamina Regen", "$EJ_stamina_regen_header", "$EJ_stamina_regen_explained $1%.");
            API.AddGemEffect<BlockReduceStamina.Config>("Block Reduce Stamina", "$EJ_stamina_block_cost_header", "$EJ_stamina_block_cost_explained $1%.");
            API.AddGemEffect<WeaponReducedStamina.Config>("Weapon Reduced Stamina", "$EJ_stamina_weapon_cost_header", "$EJ_stamina_weapon_cost_explained $1%.");
            API.AddGemEffect<CoinGreed.Config>("Coin Greed", "$EJ_coingreed_header", "$EJ_coingreed_pt1 $2% $EJ_coingreed_pt2 1-$1 $EJ_coingreed_pt3");
            API.AddGemEffect<CoinHoarder.Config>("Coin Hoarder", "$EJ_coinhoarder", "Increase all of your damage by a fraction of the coins you carry.");
            API.AddGemEffect<WaterResistant.Config>("Water Resistant", "$EJ_water_prevent", "$EJ_water_prevent_pt1 $1 $EJ_water_prevent_pt2");
            API.AddGemEffect<WaterFrenzy.Config>("Water Frenzy", "$EJ_water_dmg_buff", "$EJ_dmg_buff_pt1 $1% $EJ_water_dmg_buff_pt2");
            API.AddGemEffect<WaterSwiftness.Config>("Water Swiftness", "$EJ_water_speed_buff", "$EJ_speed_buff_pt1 $1% $EJ_water_speed_buff_pt2");
            API.AddGemEffect<BurningViking.Config>("Burning Viking", "$EJ_fire_speed_buff", "$EJ_speed_buff_pt1 $1% $EJ_fire_speed_buff_pt2");
            API.AddGemEffect<BurningFrenzy.Config>("Burning Frenzy", "$EJ_fire_dmg_buff", "$EJ_dmg_buff_pt1 $1% $EJ_fire_dmg_buff_pt2");
            API.AddGemEffect<ExpertFisher.Config>("Expert Fisher", "$EJ_skill_fishing", "$EJ_skill_fishing_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertMage.Config>("Expert Mage", "$EJ_skill_magic", "$EJ_skill_magic_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertHarvester.Config>("Expert Harvester", "$EJ_skill_harvest", "$EJ_skill_harvest_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertBrawler.Config>("Expert Brawler", "$EJ_brawling", "$EJ_brawling_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertAcrobat.Config>("Expert Acrobat", "$EJ_jump", "$EJ_jump_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertDaggers.Config>("Expert Daggers", "$EJ_knives", "$EJ_knives_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSwordsman.Config>("Expert Swordsman", "$EJ_swords", "$EJ_swords_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSmasher.Config>("Expert Smasher", "$EJ_maces", "$EJ_maces_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertPolearms.Config>("Expert Polearms", "$EJ_polearms", "$EJ_polearms_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSpearmaiden.Config>("Expert Spearmaiden", "$EJ_spears", "$EJ_spears_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertAxemaster.Config>("Expert Axemaster", "$EJ_axes", "$EJ_axes_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ExpertSprinter.Config>("Expert Sprinter", "$EJ_sprinter", "$EJ_sprinter_pt1 $1% $EJ_skill_higher $EJ_skill_inc_visible");
            API.AddGemEffect<ReduceWeight.Config>("Reduce Weight", "$EJ_weight_reduce", "$EJ_weight_reduce_pt1 $1% $EJ_weight_reduce_pt2");
            API.AddGemEffect<CoverOfDarkness.Config>("Cover of Darkness", "$EJ_cover_darkness", "$2% $EJ_cover_darkness_pt1 $1% $EJ_cover_darkness_pt2");
            API.AddGemEffect<EitrConversion.Config>("Eitr Conversion", "$EJ_eitr_conversion", "$2% $EJ_eitr_conversion_pt1 $1% $EJ_eitr_conversion_pt2");
            API.AddGemEffect<Retribution.Config>("Retribution", "$EJ_retribution", "$2% $EJ_retribution_pt1 $1% $EJ_retribution_pt2");
            API.AddGemEffect<StaggeringBlock.Config>("Staggering Block", "Blocking can stagger attackers.", "$1% chance to stagger your attacker.");
            API.AddGemEffect<FlamingGuard.Config>("Burning Guard", "On block chance to return fire damage.", "$1% chance to set your attacker on fire for $2% of the blocked damage.");
            API.AddGemEffect<FreezingGuard.Config>("Freezing Guard", "On block chance to  return frost damage.", "$1% chance to return frost damage for $2% of the blocked damage.");
            API.AddGemEffect<WetWorker.Config>("Wet Worker", "Reduces stamina usage when wet.", "$1% usage stamina cost reduction when wet.");
            API.AddGemEffect<EitrFused.Config>("Eitr Fused", "Uses eitr to increase damage.", "$1% increase to damage at the cost of $2 eitr per hit.");
            API.AddGemEffect<Farmer.Config>("Farmer", "Chance for bigger harvests.", "$2% chance to get $1 additional crops when harvesting.");

            API.AddGemEffect<CombatSpirit.Config>("Combat Spirit", "A spirit helps you in combat.", "A spirit aids you in combat for $1 seconds. Returns after a cooldown.");
            API.AddGemEffect<IntenseFire.Config>("Intense Fire", "An affinity for fire.", "You are +$1% fire resistant and have a higher chance to trigger Inferno.");
            API.AddGemEffect<SlipperyWhenWet.Config>("Slippery When Wet", "Water quickens you.", "You are $1% faster when wet.");
            API.AddGemEffect<SlipperyWhenWet.Config>("Waterproof", "You do not get wet.", "You do not get wet.");
            API.AddGemEffect<WeaponMaster.Config>("Weapon Master", "Experianced with weapons.", "Your skill with all weapons is $1% higher.");
            API.AddGemEffect<Spellsword.Config>("Spellsword", "Use eitr to increase weapon damage.", "$1% increase to damage at the cost of 5 eitr per hit.");
        }
    }
}
