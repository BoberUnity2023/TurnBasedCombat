using UnityEngine;

namespace Data.CameraData.PlayerCamera
{
    public abstract class PlayerCameraData : CameraData
    {
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _smoothScrollSpeed;

        [SerializeField] private ModelScroll _scrollY;
        [SerializeField] private ModelScroll _scrollZ;

        public ModelScroll ScrollY => _scrollY;
        public ModelScroll ScrollZ => _scrollZ;
        public float ScrollSpeed => _scrollSpeed;
        public float SmoothScrollSpeed => _smoothScrollSpeed;
    }

    [System.Serializable]
    public class ModelScroll
    {
        [SerializeField] private float _scrollMinDistance;
        [SerializeField] private float _scrollMaxDistance;
        public float ScrollMinDistance => _scrollMinDistance;

        public float ScrollMaxDistance => _scrollMaxDistance;
        
    }
}