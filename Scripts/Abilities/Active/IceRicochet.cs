using Anthill.Core;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using Libraries.DNV.DNVPool.Pool;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Components.VFX;
using Components.WorldTextEffect;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "IceRicochet", menuName = "Ability/Active/IceRicochet")]
    public class IceRicochet : ActiveAbility
    {
        [SerializeField] private AbilityEffect _abilityEffect;

        private IMagicDamage _magicDamage;
        private Pool<IceRicoshetVFX> _pool;
        private EffectRepository _effectFromPool;
        private List<IDamageable> _listDamaged;

        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<IceRicoshetVFX>();
            _effectFromPool = AbilityEffectPoolContainer.GetPool(_abilityEffect);
            _magicDamage = new IceDamageType(MinDamage, MaxDamage);
            _listDamaged = new List<IDamageable>();
        }

        protected override void Cast(IDamageable target)
        {
            AttackTarget(target);

            AntDelayed.Call(CastTime/3, () =>
            {
                var secondTarget = NearestDamageable(target, CastDistance);
                if (secondTarget != null)
                {
                    AttackTarget(secondTarget);
                    AntDelayed.Call(CastTime / 3, () =>
                    {
                        var thirdTarget = NearestDamageable(secondTarget, CastDistance);
                        if (thirdTarget != null)
                        {
                            AttackTarget(thirdTarget);
                        }                        
                    });            
                }                
            });
        }

        void AttackTarget(IDamageable target)
        {
            target.TakeDamage(_magicDamage);

            if (target.SideStats.HealthPoints.Value > 0)
            {
                TryApplyEffect(target);
            }

            _listDamaged.Add(target);

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
            _listDamaged.Clear();
        }

        protected override void TryApplyEffect(IDamageable target)
        {
            var effect = _effectFromPool.GetItem();
            effect.ApplyPeriodicDamage(target);
        }

        private IDamageable NearestDamageable(IDamageable target, float distance)
        {
            IDamageable output = null;
            float minDistance = Mathf.Infinity;

            var listDamageable = ListDamageable(target, distance);

            foreach (var damageable in listDamageable)
            {
                float distanceToTarget = Vector3.Distance(damageable.Position, target.Position);

                if (distanceToTarget < minDistance &&
                    !_listDamaged.Contains(damageable))
                {
                    minDistance = distanceToTarget;
                    output = damageable;
                }
            }

            return output;
        }

        private List<IDamageable> ListDamageable(IDamageable target, float distance)
        {
            var colliders = Physics.OverlapSphere(target.Position, distance);

            List<IDamageable> listDamageable = new List<IDamageable>();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out EnemyAI ai))
                    listDamageable.Add(ai.DamageAcquisitionSystem);
            }

            listDamageable.Remove(target);

            return listDamageable;
        }
    }
}
