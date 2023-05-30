using UnityEngine;

namespace Data.CameraData.PlayerCamera
{
    [CreateAssetMenu(fileName = "FreeCameraData", menuName = "Data/Camera/PlayerCamera/FreeCameraData" , order = 2)]
    public class FreeCameraData : PlayerCameraData
    {
        [SerializeField] private float _speedMove;

        [SerializeField] private float _edgeStep;
        [SerializeField] private float _keyboardStep;

        [SerializeField] private float _maxOffset;

        [SerializeField] private float _startOffset;

        public float SpeedMove => _speedMove;

        public float EdgeStep => _edgeStep;

        public float KeyboardStep => _keyboardStep;

        public float MaxOffset => _maxOffset;

        public float StartOffset => _startOffset;

    }
}