using System;
using Anthill.Core;
using Anthill.Inject;
using Components.WorldText;
using DamageAcquisition;
using DNVMVC;
using DNVMVC.Controllers;
using Libraries.DNV.DNVPool.PoolContainer;
using Libraries.DNV.MVC.Core;
using Stats;
using Stats.Side;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace Abilities
{
    public abstract class ActiveAbility : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _castTime;
        [SerializeField] private int _cooldown;
        [SerializeField] private float _actionPointsPrice;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;
        [SerializeField] private int _castDistance;
        [SerializeField] private bool _useAccelerator;
        [SerializeField] private bool _isDebug;

        private ActiveAbilityHit _abilityHit;
        private int _currentCooldown;
        private Action _abilityCompleted;
        private ActionPoints _actionPoints;
        private Accuracy _accuracy;


        public Sprite Icon => _icon;
        public float Accelerator { get; set; }
        public bool UseAccelerator => _useAccelerator;
        public bool IsDebug { get => _isDebug; set => _isDebug = value; }

        private WorldTextVision _worldTextVision;

        private bool IsCooldownOver => _currentCooldown < 1;
        private bool IsEnoughActionPointsToUse => _actionPoints.Value - _actionPointsPrice >= 0;
        private bool _isAbilityCompleted;

        protected float MinDamage => _minDamage;
        protected float MaxDamage => _maxDamage;
        protected float CastDistance => _castDistance;
        protected float CastTime => _castTime;

        protected WorldTextVision WorldTextVision => _worldTextVision;
        protected IOwnerSystemUsingAbility OwnerSystemUsingAbility { get; set; }

        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }
        [Inject] public AbilityEffectPoolContainer AbilityEffectPoolContainer { get; set; }

        public void Init(ActionPoints actionPoints, Action abilityCompleted, IOwnerSystemUsingAbility ownerSystemUsingAbility, Accuracy accuracy)
        {
            AntInject.Inject(this);
            OwnerSystemUsingAbility = ownerSystemUsingAbility;
            _actionPoints = actionPoints;
            _abilityCompleted = abilityCompleted;
            _currentCooldown = 0;
            _abilityHit = new ActiveAbilityHit();
            _worldTextVision = new WorldTextVision();
            _accuracy = accuracy;
            _isAbilityCompleted = true;

            Init();
        }

        public bool TryCast(IDamageable damageable)
        {
            if (!IsCanUseAbility(damageable) || !IsEnoughDistanceUse(damageable.Position) || !_isAbilityCompleted) return false;
            
            if (_useAccelerator && Accelerator >= 100)
            { 
                Accelerator -= 100; 
            }
            else
            {
                _actionPoints.Reduce(_actionPointsPrice); 
            }

            _isAbilityCompleted = false;  

            _currentCooldown = _cooldown;

            Cast(damageable);

            DNVUI.Get<MainUI>().GetController<BattleController>().HideNextRoundButton();

            AntDelayed.Call(_castTime, () =>
            {
                _abilityCompleted?.Invoke();
                ActionAfterAbilityCompleted();
                _isAbilityCompleted = true;
                DNVUI.Get<MainUI>().GetController<BattleController>().ShowNextRoundButton();
            });

            return true;
        }

        public void PointEnter(IDamageable damageable)
        {
            if (IsCanUseAbility(damageable) && IsEnoughDistanceUse(damageable.Position))
            {
                TargetPointEnter(damageable);
            }
        }

        public void PointExit(IDamageable damageable)
        {
            if (IsCanUseAbility(damageable))
            {
                TargetPointExit(damageable);
            }
        }

        public void RoundEnd()
        {
            _currentCooldown--;
            ActionAfterRoundEnd();
        }

        private bool IsCanUseAbility(IDamageable damageable)
        {
            if (_isDebug)
            {
                return true;
            }

            return IsCooldownOver && IsEnoughActionPointsToUse && IsCanCast(damageable);
        }

        private bool IsEnoughDistanceUse(Vector3 target)
        {
            if (_isDebug)
            {
                return true;
            }

            return Vector3.Distance(target, OwnerSystemUsingAbility.Position) <= _castDistance;
        }

        protected bool TryHit(IDamageable damageable)
        {
            return _abilityHit.TryHit(damageable.SideStats.ProtectionClass.Value, _accuracy.Value);
        }

        protected abstract void Init();
        protected abstract void Cast(IDamageable target);
        protected abstract bool IsCanCast(IDamageable damageable);
        protected abstract void ActionAfterRoundEnd();
        protected abstract void ActionAfterAbilityCompleted();
        protected abstract void TryApplyEffect(IDamageable target);

        protected virtual void TargetPointEnter(IDamageable target)
        {
        }

        protected virtual void TargetPointExit(IDamageable target)
        {
        }
    }
}