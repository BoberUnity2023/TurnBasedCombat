using Stats.Effect.NegativeEffects;
using Stats.Effect.Repository;
using Stats.SideStatsProvider.HealthPoint;
using UnityEngine;

namespace Stats.Effect
{
    public class NegativeEffect 
    {
        private readonly EffectRepository _repository;

        public EffectRepository Repository => _repository;

        public NegativeEffect()
        {
            _repository = new EffectRepository(new SideStatProviderDecorator[]
            {
                new PlagueNegativeEffect()
            });
        }
    }
}
