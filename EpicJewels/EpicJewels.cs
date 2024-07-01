using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace EpicJewels
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency("org.bepinex.plugins.jewelcrafting", BepInDependency.DependencyFlags.SoftDependency)]
    internal class EpicJewels : BaseUnityPlugin
    {
        public const string PluginGUID = "MidnightsFX.EpicJewels";
        public const string PluginName = "EpicJewels";
        public const string PluginVersion = "0.2.2";

        public static readonly ManualLogSource EJLog = BepInEx.Logging.Logger.CreateLogSource(PluginName);

        public Common.Config cfg;

        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        // public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            cfg = new Common.Config(Config);
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            EJLog.LogInfo("Let the gems flow.");
            GemEffects.EffectList.AddGemEffects();
            CustomGems.AddGems();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony harmony = new(PluginGUID);
            harmony.PatchAll(assembly);

        }
    }
}