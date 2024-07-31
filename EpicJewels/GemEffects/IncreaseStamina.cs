using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class IncreaseStamina
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
        public static class IncreaseTotalStamina
        {
            public static void Postfix(Player __instance, ref float stamina)
            {
                stamina += __instance.GetEffectPower<Config>("Increase Stamina").Power;
            }
        }
    }
}
