using EpicJewels.EffectHelpers;
using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class CoinGreed
    {
        [PublicAPI]
        [StructLayout(LayoutKind.Sequential)]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            //[InverseMultiplicativePercentagePower] public float Chance;
        }

        [HarmonyPatch(typeof(CharacterDrop), nameof(CharacterDrop.GenerateDropList))]
        public static class AddGreedToCreatureDrop
        {
            [UsedImplicitly]
            private static void Postfix(CharacterDrop __instance, ref List<KeyValuePair<GameObject, int>> __result)
            {
                float coin_greed = Player.m_localPlayer.GetEffectPower<Config>("CoinGreed").Power;
                Jotunn.Logger.LogInfo($"Coingreed enabled? {coin_greed > 0}");
                if (coin_greed > 0)
                {
                    float greedChance = 0.15f;
                    float chance_roll = UnityEngine.Random.value;
                    Jotunn.Logger.LogInfo($"Coingreed: {chance_roll} < {greedChance} = {chance_roll < greedChance}");
                    if (chance_roll < greedChance)
                    {
                        float greedAmount = UnityEngine.Random.Range(1, Math.Max(1, coin_greed));
                        Jotunn.Logger.LogInfo($"coingreed drop {greedAmount}");
                        __result.Add(new KeyValuePair<GameObject, int>(ObjectDB.instance.GetItemPrefab("Coins"), (int)greedAmount));
                    }
                }
                
            }
        }
    }
}
