using Anthill.Inject;
using Components.VFX;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

public class ShieldLost
{
    [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

    public ShieldLost()
    {
        AntInject.Inject(this);
    }

    public void StartEffect(Vector3 position, bool isPlayer)
    {
        if (!isPlayer)
            return;

        var _monoPool = ObjectPoolContainer.GetPool<ShieldLostVFX>();
        var item = _monoPool.GetItem();
        item.SetPosition(position);        
    }
}
