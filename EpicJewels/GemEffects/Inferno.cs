using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class Inferno
    {
        [PublicAPI]
        public struct Config
        {
            [MultiplicativePercentagePowerAttribute] public float Power;
        }
    }
}
