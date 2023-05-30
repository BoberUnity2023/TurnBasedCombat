using DamageEffect;
using DamageTypes.Interface;
using Resistance.Interface;
using Stats;
using UnityEngine;

namespace DamageAcquisition
{
    public interface IDamageable
    {
        public Transform Transform { get; }
        public Vector3 Position { get; }
        public SideStats SideStats { get; }

        public bool IsCanApplyPeriodicDamageEffect(AbilityEffect abilityEffect);
        public void TakeDamage(IMagicDamage damage);
        public void TakeDamage(IPhysicalDamage damage);
        public void AddPeriodicDamageEffect(AbilityEffect abilityEffect);
        public void Enter();
        public void Exit();
    }
}