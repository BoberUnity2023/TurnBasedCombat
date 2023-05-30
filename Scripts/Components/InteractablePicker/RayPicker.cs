using Anthill.Inject;
using InputControl;
using UnityEngine;

namespace Components.InteractablePicker
{
    public class RayPicker : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        private Vector3 _position;
        
        [Inject] public ServiceInput ServiceInput { get; set; }

        private void Start()
        {
            AntInject.Inject(this);
        }

        public bool TryPickObjectByType<T>(out T item, LayerMask mask) where T : class
        {
            if (TryRaycast(out var hit, mask))
            {
                if (hit.collider.TryGetComponent(out T itemComponent))
                {
                    item = itemComponent;
                    return true;
                }
            }

            item = null;
            return false;
        }

        public bool TryPickObjectByType<T>(out T item) where T : class
        {
            if (TryRaycast(out var hit))
            {
                if (hit.collider.TryGetComponent(out T itemComponent))
                {
                    item = itemComponent;
                    return true;
                }
            }

            item = null;
            return false;
        }
        
        public bool TryPickObjectPosition(out Vector3 position, LayerMask layerMask)
        {
            if (TryRaycast(out var hit, layerMask))
            {
                _position = hit.point;
                position = _position;
                return true;
            }

            position = _position;
            return false;
        }
        
        public bool TryPickObjectByType<T>(out T item, Vector3 origin, Vector3 endPosition, LayerMask layerMask, int distance) where T : class
        {
            if (TryRaycast(out var hit, origin, endPosition, layerMask, distance))
            {
                if (hit.collider.TryGetComponent(out T itemComponent))
                {
                    item = itemComponent;
                    return true;
                }
            }

            item = null;
            return false;
        }

        private Ray CastRay()
        {
            var ray = _camera.ScreenPointToRay(new Vector3(ServiceInput.CameraInput.MousePositionHorizontal.Value,
                ServiceInput.CameraInput.MousePositionVertical.Value));

            return ray;
        }

        private Ray CastRay(Vector3 origin, Vector3 direction)
        {
            return new Ray(origin, direction);
        }
        
        private bool TryRaycast(out RaycastHit hit, LayerMask layerMask)
        {
            var result = Physics.Raycast(CastRay(), out var rayHit, Mathf.Infinity, layerMask);
            hit = rayHit;
            
            return result;
        }
        
        private bool TryRaycast(out RaycastHit hit)
        {
            var result = Physics.Raycast(CastRay(), out var rayHit, Mathf.Infinity);
            
            hit = rayHit;
            return result;
        }
        
        private bool TryRaycast(out RaycastHit hit, Vector3 origin, Vector3 direction, LayerMask layerMask, int distance)
        {
            var result = Physics.Raycast(CastRay(origin, direction), out var rayHit, distance, layerMask);
            hit = rayHit;
            
            return result;
        }
    }
}