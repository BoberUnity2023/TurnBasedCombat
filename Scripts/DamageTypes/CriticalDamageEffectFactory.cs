using UnityEngine;

namespace DamageTypes
{
    public class CriticalDamageEffectFactory
    {
        public float TryApplyCriticalDamage(float baseDamage, int chance, int modificator, out bool isCriticalDamage)
        {
            var success = Random.Range(0, 100) < 50; // chance;

            if (!success)
            {
                isCriticalDamage = false;
                return baseDamage;
            }

            isCriticalDamage = true;
            return baseDamage + baseDamage * (modificator - 100f) / 100f;
        }
    }
}
