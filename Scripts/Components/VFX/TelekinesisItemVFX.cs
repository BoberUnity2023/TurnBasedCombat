using Libraries.DNV.DNVPool.Interface;
using UnityEngine;

namespace Components.VFX
{
    public class TelekinesisItemVFX : MonoBehaviour, IPoolObject
    {
        private IPool _pool;
        
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
           
            _pool.ReturnToPool(this);
        }

        public void RegisterPool(IPool pool)
        {
            _pool = pool;
        }

        public void Extract()
        {
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}