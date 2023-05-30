using UnityEngine;

namespace Stats.Effect
{
    public abstract class SideStatProviderDecorator :ISideStatProvider
    {
        protected ISideStatProvider _sideStatProvider;

        protected SideStatProviderDecorator()
        {
        }

        public void TrySetSideStatProvider(ISideStatProvider sideStatProvider)
        {
            _sideStatProvider = sideStatProvider;
        }

        public abstract float Calculate();        
    }
}