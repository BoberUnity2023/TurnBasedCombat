using UnityEngine;

namespace Stats.Effect.NegativeEffects
{
    internal class PlagueNegativeEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() - 4f;
        }
    }
}