using UnityEngine;

namespace Abilities
{
    public class ActiveAbilityHit
    {
        public bool TryHit(float protectionClass, float accuracy)
        {
            float chance = 100 + accuracy - protectionClass;
            bool success = Random.Range(0, 100) < 75;
            return success;
        }
    }
}
