using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
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
                    ItemDrop.ItemData[] mcoins = Player.m_localPlayer.m_inventory.GetAllItems().Where(val => val.m_dropPrefab.name == "Coins").ToArray();
                    // Check if the user has coins, if they do not, we don't give a bonus
                    if (mcoins.Length == 0) { return; }
                    float inv_coins = mcoins[0].m_stack;
                    float coin_hoarder_powerlevel = Player.m_localPlayer.GetEffectPower<Config>("Coin Hoarder").Power;
                    float coinHoarderBonus = inv_coins * (coin_hoarder_powerlevel / 100);
                    float damage_multiplier = (coinHoarderBonus + 100f) / 100f;
                    EpicJewels.EJLog.LogDebug($"Coinhorder bonus multipler {damage_multiplier} coinhorder bonus: {coinHoarderBonus} inv coins: {inv_coins} coinhorder power: {coin_hoarder_powerlevel}");
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
