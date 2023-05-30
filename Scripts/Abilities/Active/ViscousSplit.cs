using Anthill.Core;
using DamageAcquisition;
using DamageEffect;
using DamageTypes;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;
using Components.VFX;
using Anthill.Inject;
using Components.Player;
using Components.WorldTextEffect;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "ViscousSplit", menuName = "Ability/Active/ViscousSplit")]
    public class ViscousSplit : ActiveAbility
    {
        [SerializeField] private AbilityEffect _abilityEffect;

        private IMagicDamage _magicDamage;
        private Pool<ViscousSplitVFX> _pool;
        private EffectRepository _effectFromPool;
        private readonly float _height = 1.7f;

        [Inject] public Player Player { get; set; }

        protected override void Init()
        {
            AntInject.Inject(this);
            _pool = ObjectPoolContainer.GetPool<ViscousSplitVFX>();
            _effectFromPool = AbilityEffectPoolContainer.GetPool(_abilityEffect);
            _magicDamage = new EarthDamageType(MinDamage, MaxDamage);            
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

            PlayVFX(Player.transform.position + Vector3.up * _height, target.Position + Vector3.up * _height);
        }

        private void PlayVFX(Vector3 startPosition, Vector3 finishPosition)
        {
            var item = _pool.GetItem();
            item.SetPosition(startPosition);
            item.Fly(finishPosition, CastTime);            
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

        protected override void TryApplyEffect(IDamageable target)
        {
            var effect = _effectFromPool.GetItem();
            effect.ApplyPeriodicDamage(target);
        }

        protected override void ActionAfterAbilityCompleted()
        {
            
        }
    }
}

