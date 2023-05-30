using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DamageEffect
{
    public class EffectRepository
    {
        private readonly Queue<AbilityEffect> _container;
        private readonly AbilityEffect _originalAbilityEffect;
        
        private bool _isPopulated;
        private int _itemCount;
        public int ItemCount => _itemCount;


        public Type Type => _originalAbilityEffect.GetType();
        
        public EffectRepository(AbilityEffect originalAbilityEffect)
        {
            _container = new Queue<AbilityEffect>();
            _originalAbilityEffect = originalAbilityEffect;
        }
        
        public void Populate(int count)
        {
            if (_isPopulated)
            {
                return;
            }

            _itemCount = count;

            for (int i = 0; i < count; i++)
            {
                if (!TryCreate(_originalAbilityEffect, out var effect))
                {
                    break;
                }
                
                effect.ReturnToPool();
            }

            _isPopulated = true;
        }


        public void ReturnToPool(AbilityEffect item)
        {
            Add(item);
        }

        private void Add(AbilityEffect item)
        {
            _container.Enqueue(item);
        }
        
        public AbilityEffect GetItem()
        {
            if (_container.Count == 0)
            {
                if (TryCreate(_originalAbilityEffect, out var poolable))
                    return poolable;
                else
                    return default(AbilityEffect);
            }
            else
            {
                var abilityEffect = _container.Dequeue();

                abilityEffect.Extract();
                return abilityEffect;
            }
        }
        
        
        private bool TryCreate(AbilityEffect originalAbilityEffect, out AbilityEffect poolAbilityEffect)
        {
            var effect = Object.Instantiate(originalAbilityEffect);

            if (ReferenceEquals(effect, null))
            {
                poolAbilityEffect = null;
                return false;
            }
            
            effect.Init();
            effect.RegisterPool(this);
            poolAbilityEffect = effect;
            
            return true;
        }
    }
}