using Stats.Policy.Interfaces;

namespace Stats.Policy.NormalPolicy
{
    public class PolicyThatStatsIsOver : IPolicyThatStatsIsOver
    {
        private readonly int _minValue;
        public float Value => _minValue;

        public PolicyThatStatsIsOver(int minValue)
        {
            _minValue = minValue;
        }

        public bool IsOver(float value)
        {
            return value < _minValue;
        }
    }
}