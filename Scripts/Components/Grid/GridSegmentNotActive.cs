using Libraries.DNV.DNVPool.Interface;
using UnityEngine;

namespace Components.Grid
{
    public class GridSegmentNotActive : MonoBehaviour, IPoolObject
    {
        private IPool _pool;
        
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            _pool.ReturnToPool(this);
            ResetPosition();
        }

        public void RegisterPool(IPool pool)
        {
            _pool = pool;
        }

        public void Extract()
        {
            gameObject.SetActive(true);
        }

        public void ResetPosition()
        {
            transform.position = Vector3.zero;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
