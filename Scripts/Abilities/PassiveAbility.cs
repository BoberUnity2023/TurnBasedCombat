using Anthill.Inject;
using Libraries.DNV.DNVPool.PoolContainer;
using Stats;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace Abilities
{
    public abstract class PassiveAbility : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private int _cooldown;
        [SerializeField] private bool _isDebug;

        private IOwnerSystemUsingAbility _ownerSystemUsing;
        private SideStats _sideStats;

        private int _currentCooldown;
        private bool _isAbilityCompleted;

        private bool IsCooldownOver => _currentCooldown < 1;
        public string Title => _title;
        protected IOwnerSystemUsingAbility OwnerSystemUsing => _ownerSystemUsing;
        protected SideStats SideStats => _sideStats;

        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

        public void Init(IOwnerSystemUsingAbility ownerSystemUsingAbility, SideStats sideStats)
        {
            AntInject.Inject(this);
            _ownerSystemUsing = ownerSystemUsingAbility;
            _sideStats = sideStats;
            _currentCooldown = 0;
            Init();
        }
        
        public bool TryCast()
        {
            if (!IsCanUseAbility()) return false;
            
            _currentCooldown = _cooldown;
            Cast();
            return true;
        }
        
        public void RoundEnd()
        {
            _currentCooldown--;
            ActionAfterRoundEnd();

            if (IsCooldownOver)
            {
                Complete();
            }            
        }

        public void Complete()
        {
            ActionAfterAbilityCompleted();
        }
        
        private bool IsCanUseAbility()
        {
            if (_isDebug)
            {
                return true;
            }

            return IsCooldownOver && IsCanCast();
        }
        
        protected abstract void Init();
        protected abstract void Cast();
        protected abstract bool IsCanCast();
        protected abstract void ActionAfterAbilityCompleted();
        protected abstract void ActionAfterRoundEnd();
    }
}