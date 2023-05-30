using Anthill.Core;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using Libraries.DNV.DNVPool.Pool;
using System.Collections.Generic;
using UnityEngine;
using Components.VFX;
using Components.WorldTextEffect;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "EvilEye", menuName = "Ability/Active/EvilEye")]
    public class EvilEye : ActiveAbility
    {
        [SerializeField] private AbilityEffect _abilityEffect;

        private IMagicDamage _magicDamage;
        private Pool<EvilEyeVFX> _pool;
        private EffectRepository _effectFromPool;        

        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<EvilEyeVFX>();
            _effectFromPool = AbilityEffectPoolContainer.GetPool(_abilityEffect);
            _magicDamage = new FireDamageType(MinDamage, MaxDamage);            
        }

        protected override void Cast(IDamageable target)
        {
            AttackTarget(target);
        }

        void AttackTarget(IDamageable target)
        {
            if (TryHit(target))
            {
                target.TakeDamage(_magicDamage);

                if (target.SideStats.HealthPoints.Value > 0)
                {
                    TryApplyEffect(target);
                }
            }
            else
            {
                WorldTextVision.Show(WorldTextType.Miss, target.Position);
            }            

            PlayVFX(target.Position);
        }

        private void PlayVFX(Vector3 position)
        {
            var item = _pool.GetItem();

            item.SetPosition(position);

            AntDelayed.Call(CastTime, () =>
            {
                item.ReturnToPool();
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

        protected override void ActionAfterAbilityCompleted()
        {
            
        }

        protected override void TryApplyEffect(IDamageable target)
        {
            var effect = _effectFromPool.GetItem();
            effect.ApplyPeriodicDamage(target);
        }
    }
}

