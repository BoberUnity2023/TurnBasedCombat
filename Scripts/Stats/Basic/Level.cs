using System;
using Stats.Basic.Interface;
using Stats.Interfaces;
using Stats.Policy.Interfaces;

namespace Stats.Basic
{
    public class Level : IIncrementStatValue , IBasicStats
    {
        private float _value;

        private readonly IPolicyThatStatsIsFilled _policyThatStatsIsFilled;

        public event Action ValueChanged;
        public float Value => _value;
        
        public Level(float value, IPolicyThatStatsIsFilled policyThatStatsIsFilled)
        {
            _value = value;
            _policyThatStatsIsFilled = policyThatStatsIsFilled;
        }
       
        
        public void Increment(float value)
        {
            _value += value;

            if (_policyThatStatsIsFilled.IsFilled(value))
            {
                //clamp 
            }
        }
    }
}