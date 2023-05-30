using System;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.ActionPoints;

namespace Stats
{
    public class ActionPoints : IIncrementStatValue, IReducingStatValue
    {
        private float _value;
        private float _maxValue;
        
        public readonly IPolicyThatStatsIsOver PolicyThatStatsIsOver;
        public readonly IPolicyThatStatsIsFilled PolicyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private EffectsContainer _effectsContainer;
        private ISideStatProvider _sideStatProvider;
        
        public float Value => _value;
        public float MaxValue => _maxValue;

        public event Action Change;

        public ActionPoints(IBasicStats stats, MechanicalHands mechanicalHands, SideStatsValueFactory valueFactory,
            IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _sideStatProvider = new ActionPointsProvider(stats, mechanicalHands, valueFactory);
            _effectsContainer = new EffectsContainer(_sideStatProvider);
            PolicyThatStatsIsOver = policyThatStatsIsOver;
            PolicyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }

        public void Increment(float value)
        {
            _value += value;

            if (PolicyThatStatsIsFilled.IsFilled(_value))
            {
                _value = _maxValue;
            }

            Change?.Invoke();
        }

        public void Reduce(float value)
        {
            _value -= value;

            if (PolicyThatStatsIsOver.IsOver(_value))
            {
                _value = 0;
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

        public void Calculate()
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