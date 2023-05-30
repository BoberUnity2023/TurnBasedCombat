using Stats.Effect.PositiveEffects;
using Stats.Effect.Repository;
using UnityEngine;

namespace Stats.Effect
{
    public class PositiveEffect
    {
        private readonly EffectRepository _repository;

        public EffectRepository Repository => _repository;

        public PositiveEffect()
        {
            _repository = new EffectRepository(new SideStatProviderDecorator[]
            {
                new HolyPositiveEffect(),
                new HealthPointsPositiveEffect()
            });
        }

    }
}
