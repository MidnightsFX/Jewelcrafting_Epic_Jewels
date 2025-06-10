using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Linq;

namespace EpicJewels.GemEffects
{
    internal class CoinHoarder
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        private class IncreaseAllDamageByCoins
        {
            private static void Prefix(HitData hit)
            {
                if (hit.GetAttacker() is Player attacker)
                {
                    Player player = hit.GetAttacker() as Player;
                    if (player != null && player.GetEffectPower<Config>("Coin Hoarder").Power > 0) {
                        ItemDrop.ItemData[] mcoins = player.m_inventory.GetAllItems().Where(val => val.m_dropPrefab.name == "Coins").ToArray();
                        // Check if the user has coins, if they do not, we don't give a bonus
                        if (mcoins.Length == 0) { return; }
                        float inv_coins = 0f;
                        // count all of the coin stacks
                        foreach (var coin in mcoins) { inv_coins += coin.m_stack; }
                        float coin_hoarder_powerlevel = player.GetEffectPower<Config>("Coin Hoarder").Power;
                        // This equates to 12% at 1000 coins, with lvl 5 coinhoarder, at 
                        float coin_bonus = (float)(Math.Log10(coin_hoarder_powerlevel * inv_coins));
                        float damage_multiplier = (coin_bonus * 5.5f / 100f) + 1f;
                        // EpicJewels.EJLog.LogDebug($"Coinhorder bonus multipler {damage_multiplier} coin log bonus: {coin_bonus} inv coins: {inv_coins}");
                        // We modify each of the various possible damage types, because 0 * 1.01 is still 0
                        // This ensures the total damage and each types get the bonus
                        hit.m_damage.m_blunt *= damage_multiplier;
                        hit.m_damage.m_pierce *= damage_multiplier;
                        hit.m_damage.m_slash *= damage_multiplier;
                        hit.m_damage.m_fire *= damage_multiplier;
                        hit.m_damage.m_lightning *= damage_multiplier;
                        hit.m_damage.m_frost *= damage_multiplier;
                        hit.m_damage.m_spirit *= damage_multiplier;
                        hit.m_damage.m_poison *= damage_multiplier;
                        hit.m_damage.m_pickaxe *= damage_multiplier;
                    }

                }
            }
        }
    }
}
