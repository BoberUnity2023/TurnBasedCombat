using System;

namespace Stats.Basic.Interface
{
    public interface IBasicStats
    {
        public event Action ValueChanged;
        public float Value { get; }
    }
}