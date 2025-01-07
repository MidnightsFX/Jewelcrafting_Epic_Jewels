using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class ReduceWeight
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        public static float ReduceWeightByPower(float original)
        {
            if (Player.m_localPlayer == null) { return original; }

            if (Player.m_localPlayer.GetEffectPower<Config>("Reduce Weight").Power > 0)
            {
                float weightReduceMultiplier = 100f / (Player.m_localPlayer.GetEffectPower<Config>("Reduce Weight").Power + 100f);
                // EJLog.LogInfo($"Multiplying Item Weight by {weightReduceMultiplier}");
                original *= weightReduceMultiplier;
            }
            return original;
        }

        //public float GetWeight() => this.m_shared.m_weight * (float) this.m_stack;
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetWeight))]
        public static class ReduceWeight_ItemData_GetWeight_Patch
        {
            public static void Postfix(ref float __result)
            {
                __result = ReduceWeightByPower(__result);
            }
        }

        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetNonStackedWeight))]
        public static class ReduceWeight_ItemData_GetNonstackedWeight_Patch
        {
            public static void Postfix(ref float __result)
            {
                __result = ReduceWeightByPower(__result);
            }
        }
    }
}
