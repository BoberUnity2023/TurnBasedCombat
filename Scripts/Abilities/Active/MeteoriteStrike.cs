using Anthill.Core;
using Components.VFX;
using Components.WorldTextEffect;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using DamageTypes.Interface;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "MeteoriteStrike", menuName = "Ability/Active/MeteoriteStrike")]
    public class MeteoriteStrike : ActiveAbility
    {
        [SerializeField] private AbilityEffect abilityEffect;
        private IPhysicalDamage _physicalDamage;
        private Pool<MeteoriteStrikeVFX> _pool;
        private EffectRepository _effectPoolContainer;
        private MeteoriteStrikeVFX _abilityEffectVFX;
        private IDamageable _target;
        
        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<MeteoriteStrikeVFX>();
            _effectPoolContainer = AbilityEffectPoolContainer.GetPool(abilityEffect);
            _physicalDamage = new CrushingDamageType(MinDamage, MaxDamage, 100, 250);
        }

        protected override void Cast(IDamageable target)
        {
            _target = target;
            _abilityEffectVFX = _pool.GetItem();
            _abilityEffectVFX.SetPosition(_target.Position);
        }

        protected override bool IsCanCast(IDamageable damageable)
        {
            return true;
        }

        protected override void ActionAfterRoundEnd()
        {
            
        }

        protected override void ActionAfterAbilityCompleted()
        {
            Debug.Log("ActionAfterAbilityCompleted");
            
            if (TryHit(_target))
            {
                _target.TakeDamage(_physicalDamage);
                if (_target.SideStats.HealthPoints.Value > 0)
                {
                    TryApplyEffect(_target);
                }
            }
            else
            {
                WorldTextVision.Show(WorldTextType.Miss, _target.Position);
            }
            
            _abilityEffectVFX.ReturnToPool();
            _abilityEffectVFX = null;
            _target = null;
        }

        protected override void TryApplyEffect(IDamageable target)
        {
            _effectPoolContainer.GetItem().ApplyPeriodicDamage(target);
        }
    }
}