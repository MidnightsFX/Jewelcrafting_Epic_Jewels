using BepInEx.Configuration;

namespace EpicJewels.Common
{
    internal class Config
    {
        public static ConfigFile cfg;
        public static ConfigEntry<bool> EnableItemTooltipDisplay;

        public Config(ConfigFile cfgref)
        {
            // Init with the default plugin config file
            cfg = cfgref;
            cfg.SaveOnConfigSet = true;
            CreateConfigValues(cfgref);
        }

        private void CreateConfigValues(ConfigFile Config)
        {
            EnableItemTooltipDisplay = Config.Bind("Client config", "EnableItemTooltipDisplay", true,
               new ConfigDescription("Enables displaying some stat modifications on the item tooltip.",
               null));
        }
    }
}
