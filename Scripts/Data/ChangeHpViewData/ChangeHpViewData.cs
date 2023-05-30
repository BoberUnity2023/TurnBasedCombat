using UnityEngine;

namespace Components.UI.ChangeHpViewData
{
    [CreateAssetMenu(fileName = "ChangeHpView", menuName = "Data/UI/ChangeHpView")]
    public class ChangeHpViewData : ScriptableObject
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Color _colorAdd;
        [SerializeField] private Color _colorRemove;

        public Vector2 Offset => _offset;

        public Color ColorAdd => _colorAdd;

        public Color ColorRemove => _colorRemove;
    }
}