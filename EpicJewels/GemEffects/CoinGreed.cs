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
            [InverseMultiplicativePercentagePower] public float Chance;
        }

        [HarmonyPatch(typeof(Character), nameof(Character.OnDeath))]
        public static class AddGreedOnDeathFromPlayer
        {
            [UsedImplicitly]
            private static void Postfix(Character __instance)
            {
                if (__instance.m_lastHit == null) { return; }
                Player player = __instance.m_lastHit.GetAttacker() as Player;
                if (player == null) { return; }
                float coin_greed = player.GetEffectPower<Config>("Coin Greed").Power;
                // EpicJewels.EJLog.LogDebug($"Coingreed enabled? {coin_greed > 0}");
                if (coin_greed > 0)
                {
                    float greedChance = (player.GetEffectPower<Config>("Coin Greed").Chance / 100);
                    float chance_roll = UnityEngine.Random.value;
                    // EpicJewels.EJLog.LogDebug($"Coingreed: {chance_roll} < {greedChance} = {chance_roll < greedChance}");
                    if (chance_roll < greedChance)
                    {
                        float greedAmount = UnityEngine.Random.Range(1, Math.Max(1, coin_greed));
                        // EpicJewels.EJLog.LogDebug($"coingreed drop {(int)greedAmount}");
                        GameObject coin = UnityEngine.Object.Instantiate(ObjectDB.instance.GetItemPrefab("Coins"), __instance.gameObject.transform.position, __instance.gameObject.transform.rotation);
                        coin.GetComponent<ItemDrop>().m_itemData.m_stack = (int)greedAmount;
                    }
                }
                
            }
        }
    }
}
