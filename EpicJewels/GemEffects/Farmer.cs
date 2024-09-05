using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    public static class Farmer
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [AdditivePowerAttribute] public float Chance;
        }

        static List<String> UnallowedGreenThumbPickables = new List<String>() { "SurtlingCore", "Flint", "Wood", "Stone", "Amber", "AmberPearl", "Coins", "Ruby" };

        [HarmonyPatch(typeof(Pickable), nameof(Pickable.Interact))]
        public static class IncreaseCarryWeight
        {
            
            public static void Postfix(ref bool __result, Humanoid character, Pickable __instance)
            {
                if (__result == false)
                {
                    // No picking happening
                    return;
                }
                // Being picked by the current player
                if( character != null && character is Player && Player.m_localPlayer.GetEffectPower<Config>("Farmer").Power > 0) 
                {
                    string prefabname = __instance.m_itemPrefab.name.Replace("(Clone)","").Replace("Pickable_","");
                    if (UnallowedGreenThumbPickables.Contains(prefabname))
                    {
                        EpicJewels.EJLog.LogDebug($"Pickable type ({prefabname}) is not allowed for farmer perk.");
                        return;
                    }
                    float roll = UnityEngine.Random.value;
                    float chance_max = (Player.m_localPlayer.GetEffectPower<Config>("Farmer").Chance / 100);
                    EpicJewels.EJLog.LogDebug($"Farmer chance roll: {roll} < {chance_max}");
                    if (roll < chance_max)
                    {
                        int offset = 0;
                        for (int i = 0; i < Player.m_localPlayer.GetEffectPower<Config>("Farmer").Power; i++)
                        {
                            __instance.Drop(__instance.m_itemPrefab, offset++, 1);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Player), nameof(Player.Update))]
        public static class AutoPickupNearby_Pickables
        {
            private static readonly int pickableMask = LayerMask.GetMask("piece_nonsolid", "item", "Default_small");

            private static float fdt = Time.fixedDeltaTime;
            private static float last_update = 0f;
            private static float current_tick_time = 0f;
            public static void Postfix(Player __instance)
            {
                if (Player.m_localPlayer != null && __instance == Player.m_localPlayer && Player.m_localPlayer.GetEffectPower<Config>("Farmer").Power > 0)
                {
                    // We only want to do this silly expensive update once a second
                    // EpicJewels.EJLog.LogDebug($"Farmer autopick: {current_tick_time} > {last_update + 1f}");
                    current_tick_time += fdt;
                    if (current_tick_time > (last_update + 1f)) { last_update = current_tick_time; } else { return; }
                    foreach (Collider obj_collider in Physics.OverlapSphere(__instance.transform.position, 3f, pickableMask))
                    {
                        Pickable pickable_item = obj_collider.GetComponent<Pickable>() ?? obj_collider.transform.parent?.GetComponent<Pickable>();
                        if (pickable_item != null)
                        {
                            string prefabname = pickable_item.name.Replace("(Clone)", "").Replace("Pickable_", "");
                            if (!UnallowedGreenThumbPickables.Contains(prefabname))
                            {
                                
                                if (pickable_item.CanBePicked())
                                {
                                    EpicJewels.EJLog.LogDebug($"Autopicking: {prefabname}");
                                    pickable_item.Interact(Player.m_localPlayer, false, false);
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
