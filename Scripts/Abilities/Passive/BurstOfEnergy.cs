using Anthill.Core;
using Components.VFX;
using Libraries.DNV.DNVPool.Pool;
using Stats.Effect;
using Stats.Effect.PositiveEffects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "BurstOfEnergy", menuName = "Ability/Passive/BurstOfEnergy")]
    public class BurstOfEnergy : PassiveAbility
    {
        [SerializeField] private float _vfxTime;

        private SideStatProviderDecorator _sideStatProviderDecorator;        
        private Pool<BurstOfEnergyVFX> _pool;
        private bool _used;

        protected override void Init()
        {            
            _sideStatProviderDecorator = new EnergyPositiveEffect();            
            _pool = ObjectPoolContainer.GetPool<BurstOfEnergyVFX>();            
        }

        protected override void Cast()
        {            
            
            float hp = OwnerSystemUsing.Damageable.SideStats.HealthPoints.Value;
            float hpMax = OwnerSystemUsing.Damageable.SideStats.HealthPoints.MaxValue * 50 / 100;
            Debug.Log("Passive ability: " + name + " Cast: hp: " + hp + "; hpMax" + hpMax);
            
            if (_used && hp > hpMax)
            {
                _used = false;
            }

            if (!_used && hp < hpMax)
            {
                _used = true;
                Debug.Log("Passive ability " + name + " used");
                SideStats.ActionPoints.AddEffect(_sideStatProviderDecorator);

                PlayVFX(OwnerSystemUsing.Damageable.Position);
            }
        }

        protected override bool IsCanCast()
        {
            return true;
        }

        protected override void ActionAfterAbilityCompleted()
        {
            SideStats.ActionPoints.RemoveEffect(_sideStatProviderDecorator);
        }

        protected override void ActionAfterRoundEnd()
        {
            
        }

        private void PlayVFX(Vector3 position)
        {
            var item = _pool.GetItem();

            item.SetPosition(position);

            AntDelayed.Call(_vfxTime, () =>
            {
                item.ReturnToPool();
            });
        }
    }
}