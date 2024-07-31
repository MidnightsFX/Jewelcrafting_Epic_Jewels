using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class FireResistance
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
        public static class ReducePierceDamageTaken
        {
            [UsedImplicitly]
            private static void Prefix(Character __instance, HitData hit)
            {
                if (__instance is Player player && hit.GetAttacker() is { } attacker && attacker != __instance)
                {
                    if (hit.m_damage.m_fire > 0)
                    {
                        if (player.GetEffectPower<Config>("Fire Resistance").Power > 0 || player.GetEffectPower<Config>("Intense Fire").Power > 0)
                        {
                            float dmg_reduce = ((100f - (player.GetEffectPower<Config>("Fire Resistance").Power + player.GetEffectPower<Config>("Intense Fire").Power)) / 100f);
                            EpicJewels.EJLog.LogDebug($"Fire Resistance is reducing fire damage {(1 - dmg_reduce)}");
                            hit.m_damage.m_fire *= dmg_reduce;
                        }
                    }
                }
            }
        }
    }
}
