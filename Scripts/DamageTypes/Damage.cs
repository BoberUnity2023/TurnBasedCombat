using UnityEngine;

namespace DamageTypes
{
    public abstract class Damage
    {
        private readonly float _minValue;
        
        private readonly float _maxValue;          
        
        protected Damage(float minValue, float maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;            
        } 

        public float GetDamage()
        {
            float damage = Random.Range(_minValue, _maxValue);
            return damage;
        }
    }
}