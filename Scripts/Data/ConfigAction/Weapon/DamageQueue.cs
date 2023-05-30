using System.Collections;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class DamageQueue : IEnumerator
    {
        [SerializeField] private int[] _percentDamage;

        private int _position = -1;
        public bool MoveNext()
        {
            if (_position < _percentDamage.Length - 1)
            {
                _position++;
                return true;
            }

            return false;
        }
        public void Reset() => _position = -1;

        public object Current => Mathf.Clamp(_position, 0, _percentDamage.Length - 1);
        public int Percent => _percentDamage[_position];
    }
}