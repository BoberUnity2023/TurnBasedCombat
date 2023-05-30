using Anthill.Inject;
using Components.VFX;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

namespace Components.Death
{
    public class Death
    {
        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

        public Death()
        {
            AntInject.Inject(this);
        }

        public void StartEffect(Vector3 position)
        {
            var _monoPool = ObjectPoolContainer.GetPool<DeathVFX>();
            var item = _monoPool.GetItem();
            item.SetPosition(position);
            item.PlayEffect();
        }
    }
}

