using EpicJewels.EffectHelpers;
using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class WaterFrenzy
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        private static int wetstatus = "Wet".GetStableHashCode();

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        private class AddBonusSlashDamage
        {
            private static void Prefix(HitData hit)
            {
                if (hit.GetAttacker() is Player)
                {
                    bool thisVikingisWet = Player.m_localPlayer.GetSEMan().HaveStatusEffect(wetstatus);
                    EpicJewels.EJLog.LogDebug($"Frenzy checking for wet viking {thisVikingisWet}");
                    if (thisVikingisWet)
                    {
                        float wet_damage_bonus = (Player.m_localPlayer.GetEffectPower<Config>("WaterFrenzy").Power + 100) / 100;
                        EpicJewels.EJLog.LogDebug($"WetDogViking Damage multiplier {wet_damage_bonus}");
                        hit.m_damage.m_blunt *= wet_damage_bonus;
                        hit.m_damage.m_pierce *= wet_damage_bonus;
                        hit.m_damage.m_pierce *= wet_damage_bonus;
                        hit.m_damage.m_fire *= wet_damage_bonus;
                        hit.m_damage.m_lightning *= wet_damage_bonus;
                        hit.m_damage.m_frost *= wet_damage_bonus;
                        hit.m_damage.m_spirit *= wet_damage_bonus;
                        hit.m_damage.m_poison *= wet_damage_bonus;
                        hit.m_damage.m_pickaxe *= wet_damage_bonus;
                    }
                }
            }
        }
    }
}
