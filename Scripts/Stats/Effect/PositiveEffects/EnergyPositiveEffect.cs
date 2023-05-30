using UnityEngine;

namespace Stats.Effect.PositiveEffects
{
    public class EnergyPositiveEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() + 1f;
        }
    }
}
