using UnityEngine;

namespace Stats.Effect.PositiveEffects
{
    public class HealthPointsPositiveEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() + 25f;
        }
    }
}