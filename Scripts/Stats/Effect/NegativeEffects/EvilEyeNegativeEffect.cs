using UnityEngine;

namespace Stats.Effect.NegativeEffects
{
    internal class EvilEyeNegativeEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() * 0.85f;
        }
    }
}
