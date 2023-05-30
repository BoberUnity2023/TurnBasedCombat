using UnityEngine;

namespace Components.CustomCursor
{
    public class GameCursor : MonoBehaviour
    {
        [SerializeField] private GameCursorData _data;          

        private void Start()
        {            
            SetCursorState(CursorState.Default);
        }

        public void SetCursorState(CursorState state)
        {
            foreach (CursorPropetries props in _data.Propetries)
            {
                if (props.State == state)
                {                    
                    Cursor.SetCursor(props.Texture, Vector2.zero, props.Mode);
                }
            }
        }
    }
}
