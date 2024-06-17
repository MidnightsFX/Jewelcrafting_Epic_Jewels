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
                        if (player.GetEffectPower<Config>("FireResistance").Power > 0)
                        {
                            float dmg_reduce = ((100f - player.GetEffectPower<Config>("FireResistance").Power) / 100f);
                            if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"Fire Resistance is reducing fire damage {(1 - dmg_reduce)}"); }
                            hit.m_damage.m_fire *= dmg_reduce;
                        }
                    }
                }
            }
        }
    }
}
