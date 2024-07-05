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
            API.AddGemEffect<BluntResistance.Config>("BluntResistance", "Reduces taken blunt damage.", "Reduces blunt damage taken by $1%.");
            API.AddGemEffect<PierceResistance.Config>("PierceResistance", "Reduces taken pierce damage.", "Reduces pierce damage taken by $1%.");
            API.AddGemEffect<SlashResistance.Config>("SlashResistance", "Reduces taken slash damage.", "Reduces slash damage taken by $1%.");
            API.AddGemEffect<FireResistance.Config>("FireResistance", "Reduces taken fire damage.", "Reduces fire damage taken by $1%.");
            API.AddGemEffect<PoisonResistance.Config>("PoisonResistance", "Reduces taken poison damage.", "Reduces poison damage taken by $1%.");
            API.AddGemEffect<LightningResistance.Config>("LightningResistance", "Reduces taken lightning damage.", "Reduces lightning damage taken by $1%.");
            API.AddGemEffect<AddBluntDamage.Config>("AddBluntDamage", "Adds blunt damage.", "Increases blunt damage done by $1%.");
            API.AddGemEffect<AddPierceDamage.Config>("AddPierceDamage", "Adds pierce damage.", "Increases pierce damage done by $1%.");
            API.AddGemEffect<AddSlashDamage.Config>("AddSlashDamage", "Adds slash damage.", "Increases slash damage done by $1%.");
            API.AddGemEffect<AddSpiritDamage.Config>("AddSpiritDamage", "Adds spirit damage.", "Increases spirit damage done by $1%.");
            API.AddGemEffect<AddLightningDamage.Config>("AddLightningDamage", "Adds lightning damage.", "Increases lightning damage done by $1%.");
            API.AddGemEffect<AddPickaxeDamage.Config>("AddPickaxeDamage", "Adds pickaxe damage.", "Increases pickaxe damage done by $1%.");
            API.AddGemEffect<AddChopDamage.Config>("AddChopDamage", "Adds woodcutting damage.", "Increases woodcutting damage done by $1%.");
            API.AddGemEffect<Inferno.Config>("Inferno", "Chance to do massive fire damage.", "20% chance to do $1% of total damage as fire damage.");
            API.AddGemEffect<IncreaseEitr.Config>("IncreaseEitr", "Increase max Eitr.", "Increases your total eitr by $1%.");
            API.AddGemEffect<IncreaseStamina.Config>("IncreaseStamina", "Increase max stamina.", "Increases your total stamina by $1%.");
            API.AddGemEffect<IncreaseStaminaRegen.Config>("IncreaseStaminaRegen", "Increase stamina rate.", "Increases your stamina regeneration rate by $1%.");
            API.AddGemEffect<BlockReduceStamina.Config>("BlockReduceStamina", "Reduces stamina cost for blocking.", "Reduces stamina block drain by $1%.");
            API.AddGemEffect<WeaponReducedStamina.Config>("WeaponReducedStamina", "Reduces stamina cost for attacking.", "Reduces stamina attack cost by $1%.");
            API.AddGemEffect<CoinGreed.Config>("CoinGreed", "Enemies may drop coins", "Enemies have a 15% chance to drop 1-$1 coins.");
            API.AddGemEffect<CoinHoarder.Config>("CoinHoarder", "Increase your damage by carrying coins", "Increase all of your damage based on $1% of the total coins you carry.");
            API.AddGemEffect<WaterResistant.Config>("WaterResistant", "Prevents wetness for a period of time", "Prevents becoming wet if you are not exposed to water for more than $1 seconds.");
            API.AddGemEffect<WaterFrenzy.Config>("WaterFrenzy", "Increases damage when wet", "You deal $1% percent more damage when wet.");
            API.AddGemEffect<WaterSwiftness.Config>("WaterSwiftness", "Increases speed when wet", "You move $1% percent faster when wet.");
            API.AddGemEffect<BurningViking.Config>("BurningViking", "Increases speed when burning", "You move $1% percent faster when burning.");
            API.AddGemEffect<BurningFrenzy.Config>("BurningFrenzy", "Increase damage when burning", "You deal $1% percent more damage when burning.");
            API.AddGemEffect<ExpertFisher.Config>("ExpertFisher", "Increase Fishing skill", "Your fishing skill is $1% higher.");
            API.AddGemEffect<ExpertMage.Config>("ExpertMage", "Increase Magic skills", "Your Elemental & Blood magic skills are $1% higher.");
            API.AddGemEffect<ExpertHarvester.Config>("ExpertHarvester", "Increase Woodcutting & Pickaxe skill", "Your woodcutting and pickaxe skills are $1% higher.");
            API.AddGemEffect<ExpertBrawler.Config>("ExpertBrawler", "Increase Brawling skill", "Your fists skill is $1% higher.");
            API.AddGemEffect<ExpertAcrobat.Config>("ExpertAcrobat", "Increase Jump skill", "Your jump skill is $1% higher.");
            API.AddGemEffect<ExpertDaggers.Config>("ExpertDaggers", "Increase Knives skill", "Your knives skill is $1% higher.");
            API.AddGemEffect<ExpertSwordsman.Config>("ExpertSwordsman", "Increase Swords skill", "Your sword skill is $1% higher.");
            API.AddGemEffect<ExpertSmasher.Config>("ExpertSmasher", "Increase Maces skill", "Your clubs skill is $1% higher.");
            API.AddGemEffect<ExpertPolearms.Config>("ExpertPolearms", "Increase Polearms skill", "Your polearms skill is $1% higher.");
            API.AddGemEffect<ExpertSpearmaiden.Config>("ExpertSpearmaiden", "Increase Spears skill", "Your spears skill is $1% higher.");
            API.AddGemEffect<ExpertAxemaster.Config>("ExpertAxemaster", "Increase Axe skill", "Your axes skill is $1% higher.");
            API.AddGemEffect<ExpertShieldbearer.Config>("ExpertShieldbearer", "Increase Block skill", "Your block skill is $1% higher.");
            API.AddGemEffect<ExpertSprinter.Config>("ExpertSprinter", "Increase Run skill", "Your run skill is $1% higher.");
            API.AddGemEffect<ReduceWeight.Config>("ReduceWeight", "Reduces Weight of items you carry", "Carried items are $1% lighter.");
            API.AddGemEffect<CoverOfDarkness.Config>("CoverOfDarkness", "Summon bats when fighting", "15% Chance to summon a bat that fights for you for $1% seconds, stronger bats with higher levels.");
        }
    }
}
