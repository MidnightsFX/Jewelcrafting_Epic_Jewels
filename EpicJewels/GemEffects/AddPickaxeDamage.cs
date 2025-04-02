using JetBrains.Annotations;
using Jewelcrafting;

namespace EpicJewels.GemEffects
{
    internal class AddPickaxeDamage
    {
        [PublicAPI]
        public struct Config
        {
            [AdditivePowerAttribute] public float Power;
            [AdditivePowerAttribute] public float Chance;
        }
    }
}
