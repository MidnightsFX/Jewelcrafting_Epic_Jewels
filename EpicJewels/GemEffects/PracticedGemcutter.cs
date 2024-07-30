using JetBrains.Annotations;
using Jewelcrafting;
using UnityEngine;
using static Skills;

namespace EpicJewels.GemEffects
{
    public static class PracticedGemcutter
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [UsedImplicitly]
        private static void Postfix(Skills __instance, SkillType skillType, ref float __result)
        {
            // Only applies to to gemcutting
            if (skillType.ToString() != "gemcutting") { return; }
            if (Player.m_localPlayer == null) { return; }
            __result += Mathf.RoundToInt(Player.m_localPlayer.GetEffectPower<Config>("PracticedGemcutter").Power);
        }
    }
}
