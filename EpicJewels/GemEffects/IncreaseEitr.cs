using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class IncreaseEitr
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
        public static class IncreaseTotalStamina
        {
            public static void Postfix(Player __instance, ref float eitr)
            {
                eitr += __instance.GetEffectPower<Config>("Increase Eitr").Power;
            }
        }
    }
}
