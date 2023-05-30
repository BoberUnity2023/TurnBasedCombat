using UnityEngine;

namespace Components.CustomCursor
{
    [CreateAssetMenu(fileName = "GameCursor", menuName = "Data/GameCursor")]
    public class GameCursorData : ScriptableObject
    {
        [SerializeField] private CursorPropetries[] _propetries;

        public CursorPropetries[] Propetries => _propetries;
    }
}