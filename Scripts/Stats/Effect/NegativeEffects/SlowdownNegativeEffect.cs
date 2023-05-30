using UnityEngine;

namespace Stats.Effect.NegativeEffects
{
    internal class SlowdownNegativeEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() - 1f;
        }
    }
}
