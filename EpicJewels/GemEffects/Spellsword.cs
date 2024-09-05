using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    public static class Spellsword
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
        }
    }
}
