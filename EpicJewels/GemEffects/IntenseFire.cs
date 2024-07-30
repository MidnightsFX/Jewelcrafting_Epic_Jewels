using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class IntenseFire
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }
    }


}
