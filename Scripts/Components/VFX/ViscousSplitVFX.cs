using Libraries.DNV.DNVPool.Interface;
using UnityEngine;
using DG.Tweening;

namespace Components.VFX
{
    public class ViscousSplitVFX : MonoBehaviour, IPoolObject
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

        public void Fly(Vector3 finishPosition, float time)
        {            
            transform.DOJump(finishPosition, 2, 1, time);            
        }
    }
}