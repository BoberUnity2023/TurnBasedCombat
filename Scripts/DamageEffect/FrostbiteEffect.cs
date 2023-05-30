using Anthill.Inject;
using Components.VFX;
using DamageAcquisition;
using DamageTypes;
using DamageTypes.Interface;
using Libraries.DNV.DNVPool.PoolContainer;
using Resistance;
using Stats.Effect;
using Stats.Effect.NegativeEffects;
using UnityEngine;

namespace DamageEffect
{
    [CreateAssetMenu(fileName = "FrostbiteEffect", menuName = "Ability/Active/Effect/FrostbiteEffect")]
    public class FrostbiteEffect : AbilityEffect
    {
        private int _currentRound;
        private IDamageable _target;
        private IMagicDamage _damage;
        private FrostbiteEffectVFX _effectVFX;
        private SideStatProviderDecorator _statEffect;
        private Imposition _imposition;

        private EffectRepository _effectRepository;

        [Inject] public EffectVFXPoolContainer EffectVFXRepository { get; set; }

        public override void Init()
        {
            AntInject.Inject(this);

            _damage = new IceDamageType(MinDamage, MaxDamage);
            _currentRound = 0;
            _imposition = new Imposition(Chance);
            _statEffect = new PlagueNegativeEffect();
            _effectVFX = EffectVFXRepository.GetPool<FrostbiteEffectVFX>().GetItem();
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
                _effectVFX.ReturnToPool();
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
                    _target.SideStats.HealthPoints.AddEffect(_statEffect);
                    _target.AddPeriodicDamageEffect(this);
                    _effectVFX = EffectVFXRepository.GetPool<FrostbiteEffectVFX>().GetItem();
                    _effectVFX.SetPosition(_target.Position);
                    return;
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
            _target = null;
        }

        public override void ReturnToPool()
        {
            Debug.Log("ReturnToPool fro");
            _effectRepository.ReturnToPool(this);
            _effectVFX.ReturnToPool();
            _currentRound = 0;
        }

        public override void RegisterPool(EffectRepository repository)
        {
            _effectRepository = repository;
        }
    }
}