using Resistance.Interface;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.HealthPoint;
using Stats.SideStatsProvider.ProtectionFromEarth;

namespace Stats.Side
{
    public class ProtectionFromEarth : IMagicResist
    {
        private float _value;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;
        
        private EffectsContainer _effectsContainer;
        private ISideStatProvider _statsProvider;

        public float Value => _value;

        public ProtectionFromEarth(IBasicStats basicStats, Level level, SideStatsValueFactory valueFactory, IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _level = level;
            _statsProvider = new ProtectionFromEarthProvider(basicStats, level, valueFactory);
            _effectsContainer = new EffectsContainer(_statsProvider);
            _policyThatStatsIsOver = policyThatStatsIsOver;
            _policyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }


        public void AddHandlers()
        {
            _basicStats.ValueChanged += Calculate;
            _level.ValueChanged += Calculate;
        }

        public void RemoveHandlers()
        {
            _basicStats.ValueChanged -= Calculate;
            _level.ValueChanged -= Calculate;
        }

        public void Calculate()
        {
            _value = _statsProvider.Calculate();
        }

        public void AddEffect(SideStatProviderDecorator decorator)
        {
            _statsProvider = _effectsContainer.AddEffect(decorator);
            Calculate();
        }

        public void RemoveEffect(SideStatProviderDecorator decorator)
        {
            _statsProvider = _effectsContainer.RemoveEffect(decorator);
            Calculate();
        }
    }
}