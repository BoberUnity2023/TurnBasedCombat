using Anthill.Inject;
using Components.VFX;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

namespace Components.ReceiveDamage
{
    public class ReceiveDamage
    {
        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

        public ReceiveDamage()
        {
            AntInject.Inject(this);
        }

        public void StartEffect(Vector3 position, bool isPlayer)
        {
            if (isPlayer)
                StartEffectPlayer();
            else
                StartEffectEnemy(position);
        } 
        
        private void StartEffectPlayer()
        {
            var _monoPool = ObjectPoolContainer.GetPool<DamageScreenVFX>();
            var item = _monoPool.GetItem();
            UnityEngine.Camera _camera = UnityEngine.Camera.main;
            item.transform.SetParent(_camera.transform);
            item.transform.localPosition = new Vector3(0, -1, 1);
            item.transform.localRotation = Quaternion.identity;
        }

        private void StartEffectEnemy(Vector3 position)
        {
            var _monoPool = ObjectPoolContainer.GetPool<EnemyDamageVFX>();
            var item = _monoPool.GetItem();
            item.SetPosition(position);
        }
    }
}

