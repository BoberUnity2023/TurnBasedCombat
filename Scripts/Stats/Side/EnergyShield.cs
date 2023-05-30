using System;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider.EnergyShield;

namespace Stats.Side
{
    public class EnergyShield : IIncrementStatValue, IReducingStatValue
    {
        private float _value;
        private float _maxValue;
        
        public IPolicyThatStatsIsOver PolicyThatStatsIsOver;
        public IPolicyThatStatsIsFilled PolicyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private ISideStatProvider _sideStatProvider;
        public event Action Change;
        public event Action ShieldOver;
        public event Action ShieldFilled;

        public float Value => _value;
        public float MaxValue => _maxValue;
        public bool IsOver => PolicyThatStatsIsOver.IsOver(_value);

        public EnergyShield(IBasicStats basicStats, Level level, SideStatsValueFactory valueFactory,
            IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _level = level;
            _sideStatProvider = new EnergyShieldProvider(basicStats, level, valueFactory);
            PolicyThatStatsIsOver = policyThatStatsIsOver;
            PolicyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }

        public void Increment(float value)
        {
            _value += value;

            if (PolicyThatStatsIsFilled.IsFilled(_value))
            {
                //ClampValue
                ShieldFilled?.Invoke();
            }

            Change?.Invoke();
        }

        public void Reduce(float value)
        {
            _value -= value;

            if (PolicyThatStatsIsOver.IsOver(_value))
            {
                //ClampValue
                ShieldOver?.Invoke();
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
    }
}