﻿using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System.Linq;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class CombatSpirit
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }

        private static GameObject wolf = null;
        private static bool have_spirit_companion = false;
        private static float recheck_spirit_spawn_timer = 0;
        private static float cooldown_timer = 5;

        [HarmonyPatch(typeof(Player), nameof(Player.OnTargeted))]
        public static class CombatCompanion
        {
            private static void Postfix(Player __instance, bool sensed, bool alerted)
            {
                if (__instance.GetEffectPower<Config>("Combat Spirit").Power > 0 && sensed && alerted)
                {
                    if (wolf == null && have_spirit_companion == false)
                    {
                        ZNetScene.instance.m_namedPrefabs.TryGetValue("Wolf".GetStableHashCode(), out var temp);

                        Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                        Vector3 player_location = __instance.gameObject.transform.position;
                        player_location.x += 1f; // spawn next to the player
                        wolf = Object.Instantiate(temp, player_location, rotation);
                        cooldown_timer = 120;
                        have_spirit_companion = true;
                        wolf.AddComponent<CharacterTimedDestruction>();
                        Character spirit_char = wolf.GetComponent<Character>();
                        wolf.GetComponent<CharacterTimedDestruction>().m_character = spirit_char;
                        wolf.GetComponent<CharacterTimedDestruction>().Trigger(__instance.GetEffectPower<Config>("Combat Spirit").Power);
                        SkinnedMeshRenderer wolf_renderer = wolf.GetComponentInChildren<SkinnedMeshRenderer>();
                        wolf_renderer.material = EpicJewels.spiritCreature;
                        Object.Destroy(wolf.GetComponent<CharacterDrop>());
                        Object.Destroy(wolf.GetComponent<Tameable>());
                        Humanoid creature_metadata = wolf.GetComponent<Humanoid>();
                        Character creature_character = wolf.GetComponent<Character>();
                        MonsterAI creature_ai = wolf.GetComponent<MonsterAI>();
                        creature_ai.m_attackPlayerObjects = false;
                        // creature_character.m_level = 2;
                        creature_metadata.m_health = 1000; //lox health, but not resistant
                        creature_metadata.m_deathEffects.m_effectPrefabs[1].m_enabled = false;
                        creature_metadata.m_deathEffects.m_effectPrefabs[0].m_enabled = false;
                        creature_metadata.name = "EJ_spirit_wolf";
                        if (creature_metadata != null) {
                            creature_metadata.m_faction = Character.Faction.Players;
                        }
                        // Set this creatures lifetime
                        wolf.name = "Spirit Wolf";
                    }
                    //EpicJewels.EJLog.LogDebug($"checking for spawning spirit companion {recheck_spirit_spawn_timer} has companion? {have_spirit_companion} cooldown {cooldown_timer}");
                    float dt = Time.deltaTime;
                    if (cooldown_timer > 0)
                    {
                        if (dt > (cooldown_timer * 60)) { cooldown_timer = 0; }
                        cooldown_timer -= dt;
                        if (cooldown_timer < 0) { cooldown_timer = 0; }
                        return;
                    }
                    // Reduce checks for spawned spirit wolf
                    if (recheck_spirit_spawn_timer > 3) {
                        recheck_spirit_spawn_timer = 0;
                        if (Character.s_characters.Any(c => Vector3.Distance(__instance.gameObject.transform.position, c.transform.position) < 100f && c.name == "Spirit Wolf")) {
                            EpicJewels.EJLog.LogDebug("Already have spirit wolf");
                            have_spirit_companion = true;
                        } else {
                            have_spirit_companion = false;
                        }
                    } else {
                        recheck_spirit_spawn_timer += Time.deltaTime;
                    }
                }
            }
        }
    }
}
