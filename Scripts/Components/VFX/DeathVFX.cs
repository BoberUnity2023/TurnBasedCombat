using Libraries.DNV.DNVPool.Interface;
using UnityEngine;
using DG.Tweening;

namespace Components.VFX
{
    public class DeathVFX : MonoBehaviour, IPoolObject
    {        
        [SerializeField] private Transform _bones;

        private float _lifeTime;
        private IPool _pool;

        public void PlayEffect()
        {            
            _bones.localScale = Vector3.zero;
            _bones.DOScale(Vector3.one, 1).SetEase(Ease.InSine);            
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
            _lifeTime = 60f;
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