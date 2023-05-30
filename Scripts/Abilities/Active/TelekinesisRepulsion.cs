using Anthill.Core;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using DamageTypes.Interface;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Components.VFX;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "TelekinesisRepulsion", menuName = "Ability/Active/TelekinesisRepulsion")]
    public class TelekinesisRepulsion : ActiveAbility
    {
        [SerializeField] private AbilityEffect _abilityEffect;        

        private IPhysicalDamage _physicalDamage;
        private Pool<TelekinesisItemVFX> _pool;
        private EffectRepository _effectRepository;

        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<TelekinesisItemVFX>();
            _effectRepository = AbilityEffectPoolContainer.GetPool(_abilityEffect);
            _physicalDamage = new CrushingDamageType(MinDamage, MaxDamage, 170, 150);            
        }

        protected override void Cast(IDamageable target)
        {
            target.TakeDamage(_physicalDamage);            

            if (target.SideStats.HealthPoints.Value > 0)
            {
                TryApplyEffect(target);
            }

            Vector3 finishPosition = FinishPosition(target);
            target.Transform.DOMove(finishPosition, CastTime);
            PlayVFX(target.Position);

            AntDelayed.Call(CastTime, () =>
            {
                
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
            var effect = _effectRepository.GetItem();
            effect.ApplyPeriodicDamage(target);
        }

        private Vector3 FinishPosition(IDamageable target)
        {
            Vector3 vectorRepulsion = (target.Position - OwnerSystemUsingAbility.Position).normalized;        
            
            return target.Position + vectorRepulsion * CastDistance;
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

        protected override void ActionAfterAbilityCompleted()
        {
            //;
        }
    }
}