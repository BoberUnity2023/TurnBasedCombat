using Anthill.Inject;
using Components.VFX;
using DamageAcquisition;
using DamageTypes;
using DamageTypes.Interface;
using Libraries.DNV.DNVPool.PoolContainer;
using Resistance;
using UnityEngine;

namespace DamageEffect
{
    [CreateAssetMenu(fileName = "MirrorMoveEffect", menuName = "Ability/Active/Effect/MirrorMoveEffect")]
    public class MirrorMoveEffect : AbilityEffect
    {
        private int _currentRound;
        private IDamageable _target;
        
        private FlamingEffectVFX _effectVFX;

        private EffectRepository _effectRepository;

        [Inject] public EffectVFXPoolContainer EffectVFXRepository { get; set; }

        public override void Init()
        {
            AntInject.Inject(this);            
            _currentRound = 0;                    
        }

        public override void ApplyPeriodicDamage(IDamageable target)
        {
            _target = target;
            TryApplyEffect();
        }

        public override void TakeDamage()
        {            
            Debug.Log("TAKE PERIODIC DAMAGE");
        }

        public override void RoundEnd()
        {
            if (_currentRound >= CountRound)
            {
                _effectCompleted?.Invoke(this);
                _effectVFX.ReturnToPool();
                ReturnToPool();
                return;
            }

            TakeDamage();
            _currentRound++;
        }

        protected override void TryApplyEffect()
        {    
            ReturnToPool();
        }
       
        public override void Extract()
        {
            _currentRound = 0;
            _target = null;
        }

        public override void ReturnToPool()
        {
            _currentRound = 0;
            _target = null;
            _effectRepository.ReturnToPool(this);
            if (_effectVFX != null)
            {
                _effectVFX.ReturnToPool();
            }
        }

        public override void RegisterPool(EffectRepository repository)
        {
            _effectRepository = repository;
        }
    }
}