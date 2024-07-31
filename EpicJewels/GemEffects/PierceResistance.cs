using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class PierceResistance
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
                    if (hit.m_damage.m_pierce > 0)
                    {
                        if (player.GetEffectPower<Config>("Pierce Resistance").Power > 0)
                        {
                            float dmg_reduce = ((100f - player.GetEffectPower<Config>("Pierce Resistance").Power) / 100f);
                            EpicJewels.EJLog.LogDebug($"Pierce Resistance is reducing Pierce damage {(1 - dmg_reduce)}");
                            hit.m_damage.m_pierce *= dmg_reduce;
                        }
                    }
                }
            }
        }

    }
}
