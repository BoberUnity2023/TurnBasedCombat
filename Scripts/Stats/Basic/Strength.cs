using System;
using Stats.Basic.Interface;
using Stats.Interfaces;
using Stats.Policy.Interfaces;
using Stats.Policy.NormalPolicy;
using UnityEngine;

namespace Stats.Basic
{
    public class Strength : IIncrementStatValue, IReducingStatValue , IBasicStats
    {
        private float _value;

        private readonly IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IPolicyThatStatsIsOver _policyThatStatsIsOver;

        public event Action ValueChanged;
        public float Value => _value;


         public Strength(float value, IPolicyThatStatsIsFilled policyThatStatsIsFilled)
         {
             _value = value;
             _policyThatStatsIsFilled = policyThatStatsIsFilled;
             _policyThatStatsIsOver = new PolicyThatStatsIsOver(1);
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
