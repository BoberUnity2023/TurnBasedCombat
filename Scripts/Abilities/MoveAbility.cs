using UnityEngine;

namespace Abilities
{
    public abstract class MoveAbility : ScriptableObject
    {
        public abstract void Move();
        public abstract void Start();
        public abstract void Stop();
        
    }
}