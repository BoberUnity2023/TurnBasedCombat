using UnityEngine;

namespace ConfigAction.Move
{
    public abstract class  MoveBase : ScriptableObject
    {
        [SerializeField] private int _costUse;
        [SerializeField] private int _costEnd;

        public int CostUse => _costUse;
        public int CostEnd => _costEnd;
    }
}