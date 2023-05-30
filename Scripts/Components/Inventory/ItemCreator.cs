using Anthill.Inject;
using Components.Inventory;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{    
    [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

    private void Start()
    {
        AntInject.Inject(this);        
        
       
    }
}
