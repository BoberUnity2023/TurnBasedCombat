using UnityEngine;

namespace Data.CameraData.PlayerCamera
{
    [CreateAssetMenu(fileName = "LinkedCameraData", menuName = "Data/Camera/PlayerCamera/LinkedCameraData" , order = 2)]
    public class LinkedCameraData : PlayerCameraData
    {
        [SerializeField] private float _speedAround;
        

        public float SpeedAround => _speedAround;
    }
}