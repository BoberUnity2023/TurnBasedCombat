using System;
using Stats.Basic.Interface;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.Policy.NormalPolicy;

namespace Stats.Basic
{
    public class GenerationTurbine : IIncrementStatValue, IReducingStatValue, IBasicStats
    {
        private readonly IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IPolicyThatStatsIsOver _policyThatStatsIsOver;

        private float _value;

        public event Action ValueChanged;
        public float Value => _value;

        public GenerationTurbine(float value)
        {
            _value = value;
            _policyThatStatsIsFilled = new PolicyThatStatsIsFilled(50);
            _policyThatStatsIsOver = new PolicyThatStatsIsOver(0);
        }

        public void Increment(float value)
        {
            _value += value;

            if (_policyThatStatsIsFilled.IsFilled(value))
            {
                //clamp 
            }
        }

        public void Reduce(float value)
        {
            _value -= value;

            if (_policyThatStatsIsOver.IsOver(value))
            {
                //clamp 
            }
        }
    }
}