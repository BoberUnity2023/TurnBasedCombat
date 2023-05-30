using System;
using Components.InteractablePicker;
using UnityEngine;

namespace Units
{
    public class VisionGameUnit : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private RayPicker _rayPicker;

        private Vector3 _origin;
        private Vector3 _direction;
        public bool TryLookAtTarget<T>(Vector3 origin, Vector3 direction) where T : MonoBehaviour
        {
            _origin = origin + Vector3.up * 1.5f;
            _direction = direction;
            return _rayPicker.TryPickObjectByType<T>(out T item, origin + Vector3.up * 1.5f, direction, _layerMask, 20);
        }

        private void Update()
        {
            Debug.DrawRay(_origin, _direction, Color.red);
        }
    }
}
