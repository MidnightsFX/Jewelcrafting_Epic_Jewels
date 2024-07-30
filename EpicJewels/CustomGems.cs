using EpicJewels.GemEffects;
using Jewelcrafting;
using System;
using System.Text;
using UnityEngine;

namespace EpicJewels
{
    public class CustomGems
    {
        public CustomGems(AssetBundle embeddedResources)
        {
            AddGems(embeddedResources);
        }
        public void AddGems(AssetBundle embeddedResources)
        {
            Material jade = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/jade/gem_jade.mat");
            Material garnet = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/garnet/garnet_gem.mat");
            Material amber = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/amber/amber_gem.mat");
            Material opal = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/opal/opal_gem.mat");
            Material amethyst = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/amethyst/amethyst_gem.mat");
            Material aquamarine = embeddedResources.LoadAsset<Material>("assets/custom/fbx_exports/gems/aquamarine/aquamarine_gem.mat");

            API.AddGems("Jade", "jade", jade, new Color(0.031f, 0.69f, 0.043f, 1f));
            API.AddGems("Amber", "amber", amber, new Color(1f, 0.776f, 0.071f, 1f));
            API.AddGems("Aquamarine", "aquamarine", aquamarine, new Color(0.259f, 0.663f, 0.71f, 1f));
            API.AddGems("Garnet", "garnet", garnet, new Color(1f, 0.141f, 0.039f));
            API.AddGems("Opal", "opel", opal, new Color(0.945f, 0.988f, 0.988f, 1f));
            API.AddGems("Amethyst", "amethyst", amethyst, new Color(0.784f, 0.302f, 0.98f, 1f));

            API.AddGemConfig(EpicJewels.LoadEmbeddedAssetToString("EJConfig.yaml"));
        }
    }
}
