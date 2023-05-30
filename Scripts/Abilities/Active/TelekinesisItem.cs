using Anthill.Core;
using Components.Interactable.Interface;
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
using Libraries.DNV.DNVPool.Interface;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "TelekinesisItem", menuName = "Ability/Active/TelekinesisItem")]
    public class TelekinesisItem : ActiveAbility
    {
        [SerializeField] private AbilityEffect _abilityEffect;
        [SerializeField] private float _maxDistanceToItem;
        [SerializeField] private float _moveTime;
        private IPhysicalDamage _physicalDamage;
        private Pool<TelekinesisItemVFX> _pool;
        private EffectRepository _effectRepository;
        private ITelekinesable _item;        

        protected override void Init()
        {
            _pool = ObjectPoolContainer.GetPool<TelekinesisItemVFX>();
            _effectRepository = AbilityEffectPoolContainer.GetPool(_abilityEffect);
            _physicalDamage = new CrushingDamageType(MinDamage, MaxDamage, 150, 200);            
        }

        protected override void Cast(IDamageable target)
        {
            var item = NearestItem(target, CastDistance);
            if (item == null)
            {
                Debug.Log("No item in CastDistance");
                return;
            }

            item.Owner.DOJump(target.Position, 2, 1, _moveTime).SetEase(Ease.InQuad);           

            AntDelayed.Call(_moveTime, () =>
            {
                IPoolObject poolObject = item.Owner.GetComponent<IPoolObject>();
                if (poolObject != null)
                {
                    poolObject.ReturnToPool();
                }
                else
                {
                    item.Owner.gameObject.SetActive(false);
                }

                target.TakeDamage(_physicalDamage);

                if (target.SideStats.HealthPoints.Value > 0)
                {
                    TryApplyEffect(target);
                }

                PlayVFX(target.Position);                
            });
        }

        protected override void TargetPointEnter(IDamageable target)
        {            
            var item = NearestItem(target, CastDistance);
            if (item == null)
            {
                Debug.Log("No item in CastDistance");
                return;
            }
            _item = item;
            Debug.Log("Item Marked Success!");
            _item.Enter();
        }

        protected override void TargetPointExit(IDamageable target)
        {
            if (_item != null)
                _item.Exit();
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
            if (damageable == null)
                Debug.LogWarning("damageable = null");

            if (damageable != null)
                Debug.LogWarning("damageable Success");

            if (Vector3.Distance(OwnerSystemUsingAbility.Position, damageable.Position) > CastDistance)
                    return false;

            if (_item == null)
                return false;

            if (Vector3.Distance(damageable.Position, _item.Owner.position) > _maxDistanceToItem)
                return false;

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

        private ITelekinesable NearestItem(IDamageable target, float distance)
        {
            ITelekinesable output = null;
            float minDistance = Mathf.Infinity;

            var listInteractable = ListItem(target, distance);

            foreach (var interactable in listInteractable)
            {
                float distanceToTarget = Vector3.Distance(interactable.Owner.position, target.Position);

                if (distanceToTarget < minDistance)
                {
                    minDistance = distanceToTarget;
                    output = interactable;
                }
            }

            return output;
        }

        private List<ITelekinesable> ListItem(IDamageable target, float distance)
        {
            var colliders = Physics.OverlapSphere(target.Position, distance);

            List<ITelekinesable> list = new List<ITelekinesable>();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out ITelekinesable item))
                    list.Add(item);
            }
            
            return list;
        }

        protected override void ActionAfterAbilityCompleted()
        {
            //;
        }
    }
}