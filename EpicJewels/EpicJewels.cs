using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Linq;
using System;
using System.Reflection;
using UnityEngine;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using LocalizationManager;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EpicJewels
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency("org.bepinex.plugins.jewelcrafting", BepInDependency.DependencyFlags.SoftDependency)]
    internal class EpicJewels : BaseUnityPlugin
    {
        public const string PluginGUID = "MidnightsFX.EpicJewels";
        public const string PluginName = "EpicJewels";
        public const string PluginVersion = "0.9.12";

        public static readonly ManualLogSource EJLog = BepInEx.Logging.Logger.CreateLogSource(PluginName);

        internal Common.Config cfg;
        internal static AssetBundle EmbeddedResourceBundle;
        internal static Harmony Harmony = new Harmony(PluginGUID);
        public static IDeserializer yamldeserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        public static ISerializer yamlserializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).DisableAliases().Build();
        public static Material spiritCreature;

        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        // public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        public void Awake()
        {
            Localizer.Load();
            cfg = new Common.Config(Config);
            EmbeddedResourceBundle = LoadAssetBundle("EpicJewels.AssetsEmbedded.epicjewels");
            EJLog.LogDebug("Logging embedded assets.");
            foreach (string asset_name in EmbeddedResourceBundle.GetAllAssetNames())
            {
                EJLog.LogDebug(asset_name);
            }
            EJLog.LogInfo("Let the gems flow.");
            GemEffects.EffectList.AddGemEffects();
            new CustomGems(EmbeddedResourceBundle);

            spiritCreature = EmbeddedResourceBundle.LoadAsset<Material>("assets/custom/fbx_exports/gems/spirit_animal_mat.mat");

            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony.PatchAll(assembly);
        }

        public static AssetBundle LoadAssetBundle(string bundleName)
        {
            var resourceAssembly = typeof(EpicJewels).Assembly;
            string text = null;
            AssetBundle result;
            try
            {
                text = resourceAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(bundleName));
            }
            catch (Exception) {}
            using (Stream stream = resourceAssembly.GetManifestResourceStream(text))
            {
                result = AssetBundle.LoadFromStream(stream);
            }
            return result;
        }

        public static String LoadEmbeddedAssetToString(string assetName) 
        {
            var resourceAssembly = typeof(EpicJewels).Assembly;
            string text = null;
            string result;
            try
            {
                text = resourceAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(assetName));
            } catch (Exception) { }
            if (text == null) { return null; }
            using (Stream stream = resourceAssembly.GetManifestResourceStream(text))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }

        // TODO: Remove once blaxxun either sets scaling size for the synergies box or enables text fit
        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Awake))]
        public static class EnableSynergyTextfit
        {
            [HarmonyPriority(Priority.Last)]
            public static void Postfix()
            {
                IEnumerable<GameObject> objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.StartsWith("JC_Synergies_Window"));
                EJLog.LogDebug($"Found {objects.Count()} Synergy panels to update.");
                foreach (GameObject go in objects)
                {
                    // don't break crap if we can't modify a textbox
                    try
                    {
                        EJLog.LogDebug($"Updating Synergy GO {go}");
                        go.transform.Find("Bkg/Left_Text/Left_Text_1").gameObject.GetComponent<Text>().resizeTextForBestFit = true;
                    }
                    catch (Exception) { }
                }
            }
        }
        
    }
}