using Stats.Effect;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider.MoveActionPoints;

namespace Stats.Side
{
    public class MoveActionPoints :  IIncrementStatValue, IReducingStatValue
    {
        private float _value;
        private float _maxValue;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private ISideStatProvider _sideStatProvider;
   
        public float Value => _value;
        public float MaxValue => _maxValue;


        public MoveActionPoints(SideStatsValueFactory valueFactory, IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _sideStatProvider = new MoveActionPointsProvider(valueFactory);
            _policyThatStatsIsOver = policyThatStatsIsOver;
            _policyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }

        public void Calculate()
        {
            _maxValue = _sideStatProvider.Calculate();
            _value = _sideStatProvider.Calculate();
        }

        public void Increment(float value)
        {
            _value += value;

            if (_policyThatStatsIsFilled.IsFilled(_value))
            {
                _value = _maxValue;
            }
        }

        public void Reduce(float value)
        {
            _value -= value;
            
            if (_policyThatStatsIsOver.IsOver(_value))
            {
                _value = 0;
            }
        }
    }
}