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
using static CharacterDrop;

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

        [HarmonyPatch(typeof(Character), nameof(Character.OnDeath))]
        public static class AddGreedOnDeathFromPlayer
        {
            [UsedImplicitly]
            private static void Postfix(Character __instance)
            {
                if (__instance.m_lastHit.GetAttacker() != Player.m_localPlayer)
                {
                    return;
                }
                float coin_greed = Player.m_localPlayer.GetEffectPower<Config>("CoinGreed").Power;
                EpicJewels.EJLog.LogDebug($"Coingreed enabled? {coin_greed > 0}");
                if (coin_greed > 0)
                {
                    float greedChance = 0.15f;
                    float chance_roll = UnityEngine.Random.value;
                    EpicJewels.EJLog.LogDebug($"Coingreed: {chance_roll} < {greedChance} = {chance_roll < greedChance}");
                    if (chance_roll < greedChance)
                    {
                        float greedAmount = UnityEngine.Random.Range(1, Math.Max(1, coin_greed));
                        EpicJewels.EJLog.LogDebug($"coingreed drop {(int)greedAmount}");
                        for (int i = 0; i <= (int)greedAmount; i++)
                        {
                            UnityEngine.Object.Instantiate(ObjectDB.instance.GetItemPrefab("Coins"), __instance.gameObject.transform.position, __instance.gameObject.transform.rotation);
                        }
                    }
                }
                
            }
        }
    }
}
