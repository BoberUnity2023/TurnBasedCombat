using UnityEngine;
using QuickOutline;

namespace Components.OutlineEffect
{
    [CreateAssetMenu(fileName = "Outline", menuName = "Data/Outline")]
    public class OutlineData : ScriptableObject
    {
        [SerializeField] private Outline.Mode _mode;
        [SerializeField] private Color _color;
        [SerializeField] private int _width;

        public Outline.Mode Mode => _mode;

        public Color Color => _color;

        public int Width => _width;
    }

}

