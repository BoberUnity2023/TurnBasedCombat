using Anthill.Core;
using Components.VFX;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using DamageTypes.Interface;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "MirrorReflection", menuName = "Ability/Active/MirrorReflection")]

    public class MirrorReflection : ActiveAbility
    {
        [SerializeField] private AbilityEffect abilityEffect;
        
        //private Pool<HammerBlowEffect> _pool;
        private EffectRepository _effectPoolContainer;


        protected override void Init()
        {
            //_pool = ObjectPoolContainer.GetPool<HammerBlowEffect>();
            _effectPoolContainer = AbilityEffectPoolContainer.GetPool(abilityEffect);            
        }

        protected override void Cast(IDamageable target)
        {
            AntDelayed.Call(CastTime, () =>
            {
                //target.TakeDamage(_physicalDamage);
                
                if (target.SideStats.HealthPoints.Value > 0)
                {
                    TryApplyEffect(target);
                }
            });
        }

        protected override bool IsCanCast(IDamageable damageable)
        {
            return true;
        }

        protected override void ActionAfterRoundEnd()
        {
            Debug.Log("RoundEnd");
        }

        protected override void TryApplyEffect(IDamageable target)
        {
            var effect = _effectPoolContainer.GetItem();
            effect.ApplyPeriodicDamage(target);
        }

        protected override void ActionAfterAbilityCompleted()
        {
            //;
        }
    }
}
