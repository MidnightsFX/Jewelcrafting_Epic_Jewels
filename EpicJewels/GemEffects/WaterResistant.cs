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

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.AddStatusEffect), typeof(int), typeof(bool), typeof(int), typeof(float))]
        public static class Waterproof_SEMan_AddStatusEffect_Patch
        {
            public static bool Prefix(SEMan __instance, int nameHash)
            {
                if (__instance.m_character.IsPlayer() && nameHash == "Wet".GetHashCode())
                {
                    if (Player.m_localPlayer.GetEffectPower<Config>("WaterResistant").Power > 0)
                    {
                        if (Common.Config.EnableDebugMode.Value) { Jotunn.Logger.LogInfo($"Water Resistance activated."); }
                        if (delayWetTill == 0)
                        {
                            delayWetTill = Time.time + Player.m_localPlayer.GetEffectPower<Config>("WaterResistant").Power;
                        }
                        if (delayWetTill < Time.time)
                        {
                            // if the delay wet timer is more than a few seconds past, we should reset it and avoid being wet right now
                            // this happens when the player tried getting wet, but then stopped having the wet effect applied before the resistance wore off
                            // eg: the delay wet timer is from an old application of wetness
                            if ((delayWetTill + 2) < Time.time)
                            {
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
