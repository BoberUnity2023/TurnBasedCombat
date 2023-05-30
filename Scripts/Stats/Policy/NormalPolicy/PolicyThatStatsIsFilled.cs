using Stats.Policy.Interfaces;

namespace Stats.Policy.NormalPolicy
{
    public class PolicyThatStatsIsFilled : IPolicyThatStatsIsFilled
    {
        private readonly float _maxValue;

        public PolicyThatStatsIsFilled(float maxValue)
        {
            _maxValue = maxValue;
        }

        public bool IsFilled(float value)
        {
            return value >= _maxValue;
        }
    }
}