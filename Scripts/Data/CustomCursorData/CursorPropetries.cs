using System;
using UnityEngine;

namespace Components.CustomCursor
{
    [Serializable] 
    public struct CursorPropetries
    {
        [SerializeField] private CursorState _state;
        [SerializeField] private Texture2D _texture;
        [SerializeField] private CursorMode _mode;

        public CursorState State => _state;

        public Texture2D Texture => _texture;

        public CursorMode Mode => _mode;
    }    
}
