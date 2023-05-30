using System.Collections.Generic;
using System.Linq;
using Stats.SideStatsProvider.HealthPoint;

namespace Stats.Effect.Repository
{
    public class EffectRepository
    {
        private readonly List<SideStatProviderDecorator> _decorators;

        public EffectRepository(params SideStatProviderDecorator[] buffs)
        {
            _decorators = new List<SideStatProviderDecorator>(buffs);
        }

        public T GetEffect<T>() where T : SideStatProviderDecorator
        {
            return (T)_decorators.FirstOrDefault(x => x is T);
        }
    }
}
