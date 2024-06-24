using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class BluntResistance
    {

        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
        public static class ReduceBluntDamageTaken
        {
            [UsedImplicitly]
            private static void Prefix(Character __instance, HitData hit)
            {
                if (__instance is Player player && hit.GetAttacker() is { } attacker && attacker != __instance)
                {
                    if (hit.m_damage.m_blunt > 0 && player.GetEffectPower<Config>("BluntResistance").Power > 0)
                    {
                        float dmg_reduce = ((100f - player.GetEffectPower<Config>("BluntResistance").Power) / 100f);
                        EpicJewels.EJLog.LogDebug($"Blunt Resistance is reducing Blunt damage {(1 - dmg_reduce)}");
                        hit.m_damage.m_slash *= dmg_reduce;
                    }
                }
            }
        }
    }
}
