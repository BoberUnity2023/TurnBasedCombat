using UnityEngine;

namespace DamageEffect
{
    public class Imposition
    {
        private readonly float _baseChance;

        public Imposition(float baseChance)
        {
            _baseChance = baseChance;
        }

        public bool TryApplyEffects(float value)
        {
            float chance = _baseChance * (1 - value / 100);

            return Random.Range(0, 100) < chance;
        }
    }
}