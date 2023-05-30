using Anthill.Inject;
using Components.VFX;
using DamageAcquisition;
using DamageTypes;
using Libraries.DNV.DNVPool.PoolContainer;
using Stats.Effect;
using Stats.Effect.NegativeEffects;
using UnityEngine;

namespace DamageEffect
{
    [CreateAssetMenu(fileName = "FlamingEffect", menuName = "Ability/Active/Effect/FlamingEffect")]
    public class FlamingEffect : AbilityEffect
    {
        private int _currentRound;
        private IDamageable _target;
        private IMagicDamage _damage;
        private FlamingEffectVFX _effectVFX;
        private SideStatProviderDecorator _statEffect;
        private Imposition _imposition;
        private EffectRepository _effectRepository;

        [Inject] public EffectVFXPoolContainer EffectVFXRepository { get; set; }

        public override void Init()
        {
            AntInject.Inject(this);
            _damage = new FireDamageType(MinDamage, MaxDamage);
            _currentRound = 0;
            _imposition = new Imposition(Chance);
            // _statEffect = new PlagueNegativeEffect();
            _effectVFX = EffectVFXRepository.GetPool<FlamingEffectVFX>().GetItem();
        }

        public override void ApplyPeriodicDamage(IDamageable target)
        {
            _target = target;
            TryApplyEffect();
        }

        public override void TakeDamage()
        {
            _target.TakeDamage(_damage);
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
                    //_target.SideStats.HealthPoints.AddEffect(_statEffect);
                    _target.AddPeriodicDamageEffect(this);
                    _effectVFX = EffectVFXRepository.GetPool<FlamingEffectVFX>().GetItem();
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
            Debug.Log("ReturnToPool fa");
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