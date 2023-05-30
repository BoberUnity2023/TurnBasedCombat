using System.Collections.Generic;
using Stats.Effect;

namespace Stats.SideStatsProvider
{
    public class EffectsContainer
    {
        private readonly List<ISideStatProvider> _sideStatProviderDecorators;

        public EffectsContainer(ISideStatProvider sideStatProvider)
        {
            _sideStatProviderDecorators = new List<ISideStatProvider> { sideStatProvider };
        }

        public ISideStatProvider AddEffect(SideStatProviderDecorator decorator)
        {
            if (_sideStatProviderDecorators.Contains(decorator))
            {
                return GetLastEffect();
            }

            decorator.TrySetSideStatProvider(GetLastEffect());
            _sideStatProviderDecorators.Add(decorator);
            return decorator;
        }

        public ISideStatProvider RemoveEffect(SideStatProviderDecorator decorator)
        {
            if (!_sideStatProviderDecorators.Contains(decorator))
            {
                return GetLastEffect();
            }

            _sideStatProviderDecorators.Remove(decorator);

            return GetLastEffect();
        }

        private ISideStatProvider GetLastEffect()
        {
            return _sideStatProviderDecorators[_sideStatProviderDecorators.Count - 1];
        }
    }
}