using Components.WorldText.Struct;
using Components.WorldTextEffect;
using Libraries.DNV.DNVPool.Interface;
using UnityEngine;

namespace PoolItems
{
    public class WorldText : MonoBehaviour, IPoolObject
    {
        [SerializeField] private WorldTextProperty[] _repository;

        private WorldTextProperty _currentProperty;
        private float _currentLifeTime;
        private IPool _pool;

        public void ReturnToPool()
        {
            _pool.ReturnToPool(this);
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }

        public void RegisterPool(IPool pool)
        {
            _pool = pool;
        }

        public void Extract()
        {
            gameObject.SetActive(true);
            _currentLifeTime = Mathf.Infinity; 
        }

        public void SetType(WorldTextType type)
        {
            _currentProperty = CurrentPropetries(type);
            _currentLifeTime = _currentProperty.LifeTime;
            ActivateByType(type);
        }

        public WorldText SetTextValue(float value)
        {
            _currentProperty.Prefab.leftText = Mathf.Round(value).ToString();
            return this;
        }

        public WorldText SetText(string text)
        {
            _currentProperty.Prefab.leftText = text;
            return this;
        }

        public WorldText SetPosition(Vector3 position)
        {
            var newPosition = position + Vector3.up * 2;
            transform.position = newPosition;
            return this;
        }

        private void Update()
        {
            _currentLifeTime -= Time.deltaTime;

            if (_currentLifeTime < 0)
            {
                ReturnToPool();
            }
        }

        private WorldTextProperty CurrentPropetries(WorldTextType type)
        {
            foreach(var item in _repository)
            {
                if (item.Type == type)
                    return item;
            }
            return _repository[0];
        }

        private void ActivateByType(WorldTextType type)
        {            
            foreach (var item in _repository)
            {                
                var prefab = item.Prefab;
                if (item.Prefab != null)
                {
                    prefab.gameObject.SetActive(item.Type == type);                    
                }
                else
                {
                    Debug.LogWarning("WorldText item.Prefab is null: " + type.ToString());
                }                
            }            
        }
    }
}