using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System.Collections.Generic;
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
            [AdditivePowerAttribute] public float MaxCount;
        }

        public static List<ZDOID> spawnedBats = new List<ZDOID>();
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
                            if (spawnedBats.Count >= Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").MaxCount) {
                                // EpicJewels.EJLog.LogDebug($"Cover of darkness max ({Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").MaxCount}) triggered, checking spawned bats liveliness.");
                                List<ZDOID> alive_bats = new List<ZDOID>();
                                foreach(ZDOID bat in spawnedBats)
                                {
                                    GameObject bat_exists = ZNetScene.instance.FindInstance(bat);
                                    // EpicJewels.EJLog.LogDebug($"Cover of darkness checking bat dead: {(bat_exists == null)}");
                                    if (bat_exists != null && !alive_bats.Contains(bat)) {
                                        alive_bats.Add(bat);
                                    }
                                }
                                spawnedBats = alive_bats;
                                if (spawnedBats.Count >= Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").MaxCount) {
                                    return; 
                                }
                            }

                            // Spawn an extra bat above 90 power
                            if (Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power > 90)
                            {
                                SpawnBat(bat_stars);
                                SpawnBat(bat_stars);
                            } else {
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
            player_location.y += 1f; // spawn just above the player
            GameObject creature = Object.Instantiate(bat, player_location, rotation);
            Character creature_character = creature.GetComponent<Character>();
            ZNetView zNetView = creature.GetComponent<ZNetView>();
            zNetView.m_persistent = true;
            // Increase the bats level based on how strong the darkness is
            creature_character.m_level = stars;
            // Remove drops from this creature
            Object.Destroy(creature.GetComponent<CharacterDrop>());


            // Set the creatures faction to the player
            MonsterAI creature_AI = creature.GetComponent<MonsterAI>();
            if (creature_AI != null) {
                if (creature.GetComponent<Tameable>() == null) { Tameable tamable = creature.AddComponent<Tameable>(); }
                creature_AI.MakeTame();
            } else {
                // This creatures faction isn't set so destroy it
                Object.Destroy(creature);
            }

            // Set this creatures lifetime
            creature.AddComponent<CharacterTimedDestruction>();
            creature.GetComponent<CharacterTimedDestruction>().m_character = creature_character;
            creature.GetComponent<CharacterTimedDestruction>().Trigger(Player.m_localPlayer.GetEffectPower<Config>("Cover of Darkness").Power);
            spawnedBats.Add(creature_character.GetZDOID());
        }
    }

}
