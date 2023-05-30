using System;
using DamageAcquisition;
using UnityEngine;

namespace DamageEffect
{
    public abstract class AbilityEffect : ScriptableObject
    {
        [SerializeField] private int _countRound;
        [SerializeField] private float _chance;
        [SerializeField] private EffectType _effectType;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        protected Action<AbilityEffect> _effectCompleted;
        protected EffectType EffectType => _effectType;
        protected float Chance => _chance;
        protected int CountRound => _countRound;
        protected float MinDamage => _minDamage;
        protected float MaxDamage => _maxDamage;

        public void AddEffectCompletedHandlers(Action<AbilityEffect> callback)
        {
            _effectCompleted += callback;
        }

        public void RemoveEffectCompletedHandlers(Action<AbilityEffect> callback)
        {
            _effectCompleted -= callback;
        }

        public abstract void Init();
        public abstract void TakeDamage();
        public abstract void RoundEnd();
        public abstract void ApplyPeriodicDamage(IDamageable target);
        protected abstract void TryApplyEffect();
        
        public abstract void Extract();
        public abstract void ReturnToPool();
        public abstract void RegisterPool(EffectRepository repository);
    }
}