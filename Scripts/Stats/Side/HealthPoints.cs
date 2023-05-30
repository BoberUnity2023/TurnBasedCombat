using System;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.HealthPoint;

namespace Stats.Side
{
    public class HealthPoints : IIncrementStatValue, IReducingStatValue, IBuffed
    {
        private readonly IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private readonly IPolicyThatStatsIsFilled PolicyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private float _value;
        private float _maxValue;

        private EffectsContainer _effectsContainer;
        private ISideStatProvider _sideStatProvider;

        public event Action Change;
        public event Action HealthOver;
        public event Action HealthFilled;

        public float Value => _value;
        public float MaxValue => _maxValue;
        

        public HealthPoints(IBasicStats basicStats, Level level, SideStatsValueFactory valueFactory,
            IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _level = level;

            _sideStatProvider = new HealthPointsProvider(basicStats, level, valueFactory);
            _effectsContainer = new EffectsContainer(_sideStatProvider);

            _policyThatStatsIsOver = policyThatStatsIsOver;
            PolicyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }

        public void Increment(float value)
        {
            _value += value;

            if (PolicyThatStatsIsFilled.IsFilled(_value))
            {
                //ClampValue
                HealthFilled?.Invoke();
            }

            Change?.Invoke();
        }

        public void Reduce(float value)
        {
            _value -= value;

            if (_policyThatStatsIsOver.IsOver(_value))
            {
                //ClampValue
                HealthOver?.Invoke();
            }
            Change?.Invoke();
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

        private void Calculate()
        {
            _value = _sideStatProvider.Calculate();
            _maxValue = _sideStatProvider.Calculate();
        }

        public void AddEffect(SideStatProviderDecorator decorator)
        {
            _sideStatProvider = _effectsContainer.AddEffect(decorator);
            Calculate();
        }

        public void RemoveEffect(SideStatProviderDecorator decorator)
        {
            _sideStatProvider = _effectsContainer.RemoveEffect(decorator);
            Calculate();
        }
    }
}