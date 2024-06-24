using EpicJewels.EffectHelpers;
using HarmonyLib;
using JetBrains.Annotations;
using Jewelcrafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EpicJewels.GemEffects
{
    internal class WaterResistant
    {
        [PublicAPI]
        public struct Config
        {
            [InverseMultiplicativePercentagePower] public float Power;
        }

        public static float delayWetTill = 0;
        private static int wet_hash =  "Wet".GetHashCode();

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.AddStatusEffect), typeof(int), typeof(bool), typeof(int), typeof(float))]
        public static class Waterproof_SEMan_AddStatusEffect_Patch
        {
            public static bool Prefix(SEMan __instance, int nameHash)
            {
                if (__instance.m_character.IsPlayer() && nameHash == wet_hash)
                {
                    if (Player.m_localPlayer.GetEffectPower<Config>("WaterResistant").Power > 0)
                    {
                        // EpicJewels.EJLog.LogInfo($"Water Resistance activated.");
                        if (delayWetTill == 0)
                        {
                            delayWetTill = Time.time + Player.m_localPlayer.GetEffectPower<Config>("WaterResistant").Power;
                            EpicJewels.EJLog.LogDebug($"Set water resistance timeout: {delayWetTill}");
                            return false;
                        }
                        if (delayWetTill < Time.time)
                        {
                            EpicJewels.EJLog.LogDebug($"Water resistance is expired.");
                            // if the delay wet timer is more than a few seconds past, we should reset it and avoid being wet right now
                            // this happens when the player tried getting wet, but then stopped having the wet effect applied before the resistance wore off
                            // eg: the delay wet timer is from an old application of wetness
                            if ((delayWetTill + 2) < Time.time)
                            {
                                EpicJewels.EJLog.LogDebug($"Reset water resistance.");
                                delayWetTill = 0;
                                return false;
                            }
                            // Reset since the character got wet
                            delayWetTill = 0;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
