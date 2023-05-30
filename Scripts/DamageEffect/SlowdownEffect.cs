using Anthill.Inject;
using Components.VFX;
using DamageAcquisition;
using Libraries.DNV.DNVPool.PoolContainer;
using Stats.Effect;
using Stats.Effect.NegativeEffects;
using UnityEngine;

namespace DamageEffect
{
    [CreateAssetMenu(fileName = "SlowdownEffect", menuName = "Ability/Active/Effect/SlowdownEffect")]
    public class SlowdownEffect : AbilityEffect
    {
        private int _currentRound;
        private IDamageable _target;        
        private SlowdownEffectVFX _effectVFX;
        private SideStatProviderDecorator _statEffect;
        private Imposition _imposition;
        private EffectRepository _effectRepository;

        [Inject] public EffectVFXPoolContainer EffectVFXRepository { get; set; }

        public override void Init()
        {
            AntInject.Inject(this);
            
            _currentRound = 0;
            _imposition = new Imposition(Chance);
            _statEffect = new SlowdownNegativeEffect();
            _effectVFX = EffectVFXRepository.GetPool<SlowdownEffectVFX>().GetItem();
        }

        public override void ApplyPeriodicDamage(IDamageable target)
        {
            _target = target;
            TryApplyEffect();
        }

        public override void TakeDamage()
        {
            
        }

        public override void RoundEnd()
        {
            if (_currentRound >= CountRound)
            {
                _effectCompleted?.Invoke(this);
                ReturnToPool();
                return;
            }

            TakeDamage();
            _currentRound++;
        }

        protected override void TryApplyEffect()
        {
            var effectResist = 0f;

            effectResist = EffectType == EffectType.Magic
                ? _target.SideStats.MagicalResistanceEffect.Value
                : _target.SideStats.PhysicalResistanceEffect.Value;

            if (_imposition.TryApplyEffects(effectResist))
            {
                if (_target.IsCanApplyPeriodicDamageEffect(this))
                {                    
                    _target.SideStats.ActionPoints.AddEffect(_statEffect);

                    _effectVFX = EffectVFXRepository.GetPool<SlowdownEffectVFX>().GetItem();
                    _effectVFX.SetPosition(_target.Position);
                }
            }
            else
            {
                ReturnToPool();
            }
        }

        public override void Extract()
        {
            _currentRound = 0;
        }

        public override void ReturnToPool()
        {            
            _effectVFX.ReturnToPool();
            _currentRound = 0;
            _effectRepository.ReturnToPool(this);            
        }

        public override void RegisterPool(EffectRepository repository)
        {
            _effectRepository = repository;
        }
    }
}
