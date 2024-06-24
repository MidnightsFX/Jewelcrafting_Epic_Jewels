using BepInEx.Configuration;

namespace EpicJewels.Common
{
    internal class Config
    {
        public static ConfigFile cfg;
        public static ConfigEntry<bool> EnableDebugMode;

        public Config(ConfigFile cfgref)
        {
            // Init with the default plugin config file
            cfg = cfgref;
            cfg.SaveOnConfigSet = true;
            CreateConfigValues(cfgref);
        }

        private void CreateConfigValues(ConfigFile Config)
        {
            // Debugmode
            //EnableDebugMode = Config.Bind("Client config", "EnableDebugMode", false,
            //    new ConfigDescription("Enables Debug logging for Recipe Manager. This is client side and is not syncd with the server.",
            //    null));
        }
    }
}
