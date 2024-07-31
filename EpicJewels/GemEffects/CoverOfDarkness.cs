using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class CoverOfDarkness
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
            [InverseMultiplicativePercentagePower] public float Chance;
        }

        // public static Dictionary<float, GameObject> spawnedBats;
        private static GameObject bat = null;

        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        public static class SummonBatHelpers
        {
            private static void Prefix(HitData hit)
            {
                if (hit.GetAttacker() is Player)
                {
                    if (Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power > 0)
                    {
                        if (Random.value < (Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Chance/ 100))
                        {
                            EpicJewels.EJLog.LogDebug($"Cover of darkness triggered darkness power: {Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power}");
                            int bat_stars = 1; // 1 = 0 stars, 2 = 1 star etc
                            switch (Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power)
                            {
                                // Ordering here increases stars based on the first power level curve point
                                case float power when power > 70f:
                                    bat_stars = 4;
                                    break;
                                case float power when power > 50f:
                                    bat_stars = 3;
                                    break;
                                case float power when power > 30f:
                                    bat_stars = 2;
                                    break;
                            }
                            // Spawn an extra bat above 90 power
                            if (Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power > 90)
                            {
                                SpawnBat(bat_stars);
                                SpawnBat(bat_stars);
                            } else
                            {
                                SpawnBat(bat_stars);
                            }

                            
                        }
                    }
                }
            }
        }

        static void SpawnBat(int stars = 1)
        {
            // Set the reference to a bat
            if (bat == null)
            {
                ZNetScene.instance.m_namedPrefabs.TryGetValue("Bat".GetStableHashCode() ,out bat);
            }

            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Vector3 player_location = Player.m_localPlayer.gameObject.transform.position;
            player_location.z += 0.5f; // spawn just above the player
            GameObject creature = Object.Instantiate(bat, player_location, rotation);
            Character creature_character = creature.GetComponent<Character>();
            // Increase the bats level based on how strong the darkness is
            creature_character.m_level = stars;
            // Remove drops from this creature
            Object.Destroy(creature.GetComponent<CharacterDrop>());

            // Set the creatures faction to the player
            Humanoid creature_metadata = creature.GetComponent<Humanoid>();
            if (creature_metadata != null)
            {
                creature_metadata.m_faction = Character.Faction.Players;
            }
            else
            {
                // This creatures faction isn't set so destroy it
                Object.Destroy(creature);
            }

            // Set this creatures lifetime
            creature.AddComponent<CharacterTimedDestruction>();
            creature.GetComponent<CharacterTimedDestruction>().m_character = creature_character;
            creature.GetComponent<CharacterTimedDestruction>().Trigger(Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power);
        }
    }

}
