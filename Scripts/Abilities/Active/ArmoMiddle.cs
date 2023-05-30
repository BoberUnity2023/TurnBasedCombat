using Anthill.Core;
using DamageAcquisition;
using DamageTypes;
using Libraries.DNV.DNVPool.Pool;
using UnityEngine;
using Components.VFX;
using Components.WorldTextEffect;
using DamageTypes.Interface;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "ArmoMiddle", menuName = "Ability/Active/ArmoMiddle")]
    public class ArmoMiddle : ActiveAbility
    {
        private IPhysicalDamage _physicalDamage;
        private Pool<TakeDamageVFX> _pool;

        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<TakeDamageVFX>();            
            _physicalDamage = new CrushingDamageType(MinDamage, MaxDamage, 50, 250);            
        }

        protected override void Cast(IDamageable target)
        {
            AttackTarget(target);
        }

        void AttackTarget(IDamageable target)
        {
            if (TryHit(target))
            {
                target.TakeDamage(_physicalDamage);
                PlayVFX(target.Position + Vector3.up);
            }
            else
            {
                WorldTextVision.Show(WorldTextType.Miss, target.Position);
            }
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
            
        }
    }
}

