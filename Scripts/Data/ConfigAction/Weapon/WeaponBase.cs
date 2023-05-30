using UnityEngine;

namespace Items
{
    public abstract class WeaponBase : ScriptableObject
    {
        [SerializeField] private int _max;
        [SerializeField] private int _min;
        
        [SerializeField] private int _useCost;
        
        [SerializeField] private DamageQueue _damageQueue;
        public int UseCost => _useCost;
        public int Min => _min;
        public int Max => _max;
        public DamageQueue DamageQueue => _damageQueue;
        
    }
}