using Libraries.DNV.DNVPool.Interface;
using UnityEngine;

namespace Components.VFX
{
    public class ShieldLostVFX : MonoBehaviour, IPoolObject
    {   
        private float _lifeTime;
        private IPool _pool;

        public void PlayEffect()
        {
            
        }

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
            _lifeTime = 5f;
        }

        private void Update()
        {
            if (_lifeTime <= 0)
            {
                ReturnToPool();
            }
            else
            {
                _lifeTime -= Time.deltaTime;
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }

}

