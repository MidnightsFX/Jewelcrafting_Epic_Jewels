using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class Waterproof
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePower] public float Power;
        }
    }
}
