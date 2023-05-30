using UnityEngine;

namespace Extensions.Camera
{
    public static class CameraExtensions
    {
        public static Ray CastRayByScreenPosition(this UnityEngine.Camera camera, Vector3 position)
        {
            var ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(position));
            return ray;
        }
    
        public static Ray CastRay(this UnityEngine.Camera camera, Vector3 position)
        {
            var ray = camera.ScreenPointToRay(position);
            return ray;
        }
    }
}
