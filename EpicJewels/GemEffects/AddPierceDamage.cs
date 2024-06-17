using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class AddPierceDamage
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        //[HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        //private class AddBonusPierceDamage
        //{
        //    [UsedImplicitly]
        //    private static void Prefix(HitData hit)
        //    {
        //        if (hit.GetAttacker() is Player attacker)
        //        {
        //            hit.m_damage.m_blunt += attacker.GetEffectPower<Config>("AddPierceDamage").Power;
        //        }
        //    }
        //}
    }
}
