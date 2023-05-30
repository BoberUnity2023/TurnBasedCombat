using UnityEngine;

namespace Components.UI.HintWorldView
{
    [CreateAssetMenu(fileName = "HintWorldView", menuName = "Data/HintWorldView")]
    public class HintWorldViewData : ScriptableObject
    {
        [SerializeField] private Vector2 _offset;

        public Vector2 Offset => _offset;
    }
}

