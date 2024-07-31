using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class ReduceWeight
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }
        //public float GetWeight() => this.m_shared.m_weight * (float) this.m_stack;
        [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetWeight))]
        public static class ReduceWeight_ItemData_GetWeight_Patch
        {
            public static void Postfix(ref float __result)
            {
                // EJLog.LogInfo("Starting weight reduce check");
                if (Player.m_localPlayer == null) { return; }
                // if (__result <= 10) { return; } // Only make this work on bigger items?
                if (Player.m_localPlayer.GetEffectPower<Config>("Reduce Weight").Power > 0)
                {
                    float weightReduceMultiplier = 100f / (Player.m_localPlayer.GetEffectPower<Config>("Reduce Weight").Power + 100f);
                    // EJLog.LogInfo($"Multiplying Item Weight by {weightReduceMultiplier}");
                    __result *= weightReduceMultiplier;
                }
            }
        }
    }
}
