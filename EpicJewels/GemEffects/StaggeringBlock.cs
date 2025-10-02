using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class StaggeringBlock
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
        private static class StaggeringBlock_Patch
        {
            private static void Postfix(Humanoid __instance, HitData hit, Character attacker, ref bool __result)
            {
                if (__instance is Player player && player.GetEffectPower<Config>("Staggering Block").Power > 0 && __result == true && attacker != null && attacker.IsDead() == false) {
                    float roll = UnityEngine.Random.value;
                    float chance_max = (player.GetEffectPower<Config>("Staggering Block").Power / 100);
                    // EpicJewels.EJLog.LogDebug($"Staggering block chance roll: {roll} < {chance_max}");
                    if (roll < chance_max) {
                        // EpicJewels.EJLog.LogDebug($"Staggering attacker");
                        attacker.AddStaggerDamage(999f, -hit.m_dir, null);
                    }  
                }
            }
        }
    }
}
