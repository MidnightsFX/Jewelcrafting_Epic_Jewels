using EpicJewels.EffectHelpers;
using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class BurningFrenzy
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        private static int burningstatus = "Burning".GetStableHashCode();

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        private class AddBonusBonusDamageWhileOnFire
        {
            private static void Prefix(HitData hit)
            {
                if (hit.GetAttacker() is Player)
                {
                    bool thisVikingisOnFIRE = Player.m_localPlayer.GetSEMan().HaveStatusEffect(burningstatus);
                    EpicJewels.EJLog.LogDebug($"Frenzy checking for burning viking {thisVikingisOnFIRE}");
                    if (thisVikingisOnFIRE)
                    {
                        float onfire_damage_bonus = (100 + Player.m_localPlayer.GetEffectPower<Config>("Burning Frenzy").Power) / 100;
                        EpicJewels.EJLog.LogDebug($"VikingOnFire Damage multiplier {onfire_damage_bonus}");
                        hit.m_damage.m_blunt *= onfire_damage_bonus;
                        hit.m_damage.m_pierce *= onfire_damage_bonus;
                        hit.m_damage.m_pierce *= onfire_damage_bonus;
                        hit.m_damage.m_fire *= onfire_damage_bonus;
                        hit.m_damage.m_lightning *= onfire_damage_bonus;
                        hit.m_damage.m_frost *= onfire_damage_bonus;
                        hit.m_damage.m_spirit *= onfire_damage_bonus;
                        hit.m_damage.m_poison *= onfire_damage_bonus;
                        hit.m_damage.m_pickaxe *= onfire_damage_bonus;
                    }
                }
            }
        }
    }
}
