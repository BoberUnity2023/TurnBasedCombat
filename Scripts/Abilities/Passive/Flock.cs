using AI;
using Components.VFX;
using Libraries.DNV.DNVPool.Pool;
using Stats.Effect;
using Stats.Effect.PositiveEffects;
using System.Collections.Generic;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Flock", menuName = "Ability/Passive/Flock")]
    public class Flock : PassiveAbility
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _vfxTime;

        private SideStatProviderDecorator _statDecorator;
        private SideStatProviderDecorator _statDecoratorX2;
        private SideStatProviderDecorator _currentStatDecorator;

        private Pool<FlockEffectVFX> _poolX1;
        private Pool<FlockX2EffectVFX> _poolX2;

        private FlockEffectVFX _itemX1;
        private FlockX2EffectVFX _itemX2;

        protected override void Init()
        {
            _statDecorator = new FlockPositiveEffect();
            _statDecoratorX2 = new FlockPositiveEffectX2();
            _poolX1 = ObjectPoolContainer.GetPool<FlockEffectVFX>();
            _poolX2 = ObjectPoolContainer.GetPool<FlockX2EffectVFX>();
        }

        protected override void Cast()
        {            
            SideStatProviderDecorator statDecorator = CurrentStatDecorator;

            if (statDecorator != _currentStatDecorator)
            {
                if (_currentStatDecorator != null)
                {
                    SideStats.ProtectionFromCrushing.RemoveEffect(_currentStatDecorator);
                    SideStats.ProtectionFromCutting.RemoveEffect(_currentStatDecorator);
                    SideStats.ProtectionFromStabbing.RemoveEffect(_currentStatDecorator);
                    Debug.Log("Passive ability: " + name + " Cast: Del Decorator"); 
                }

                _currentStatDecorator = statDecorator;                

                if (_currentStatDecorator != null)
                {
                    SideStats.ProtectionFromCrushing.AddEffect(_currentStatDecorator);
                    SideStats.ProtectionFromCutting.AddEffect(_currentStatDecorator);
                    SideStats.ProtectionFromStabbing.AddEffect(_currentStatDecorator);
                    Debug.Log("Passive ability: " + name + " Cast: Add Decorator");
                }

                TryPlayVFX();
            }
        }

        protected override bool IsCanCast()
        {
            return true;
        }

        protected override void ActionAfterAbilityCompleted()
        {
            SideStats.HealthPoints.RemoveEffect(_statDecorator);
            TryRemoveVFX();
        }

        protected override void ActionAfterRoundEnd()
        {

        }

        private void TryPlayVFX()
        {
            TryRemoveVFX();

            if (_currentStatDecorator == _statDecorator)
            {
                _itemX1 = _poolX1.GetItem();
                _itemX1.SetPosition(OwnerSystemUsing.Damageable.Position);
            }

            if (_currentStatDecorator == _statDecoratorX2)
            {
                _itemX2 = _poolX2.GetItem();
                _itemX2.SetPosition(OwnerSystemUsing.Damageable.Position);
            }                
        }

        private void TryRemoveVFX()
        {
            if (_itemX1 != null)
                _itemX1.ReturnToPool();

            if (_itemX2 != null)
                _itemX2.ReturnToPool();
        }

        private List<IOwnerSystemUsingAbility> List(IOwnerSystemUsingAbility target, float distance)
        {
            var colliders = Physics.OverlapSphere(target.Position, distance);

            List<IOwnerSystemUsingAbility> list = new List<IOwnerSystemUsingAbility>();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out EnemyAI ai))
                    list.Add(ai.GetComponent<IOwnerSystemUsingAbility>());
            }

            list.Remove(target);

            return list;
        }

        private int CountEnemiesWithTheSameAbility
        {
            get
            {
                int count = 0;
                var list = List(OwnerSystemUsing, _distance);
                foreach (var enemy in list)
                {
                    foreach (var ability in enemy.PassiveAbilities)
                    {
                        if (ability.Title == Title)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        private SideStatProviderDecorator CurrentStatDecorator
        {
            get
            {
                int count = CountEnemiesWithTheSameAbility;
                switch (count)
                {
                    case 0:
                        return null;
                    case 1:
                        return _statDecorator;
                    default:
                        return _statDecoratorX2;
                }
            }            
        }
    }
}