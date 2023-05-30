using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.CameraData.PlayerCamera
{
    [CreateAssetMenu(fileName = "PlayerCameraDataRepository", menuName = "Data/Camera/PlayerCamera/PlayerCameraDataRepository" , order = 2)]
    public class PlayerCameraDataRepository : ScriptableObject
    {
        [SerializeField] private List<PlayerCameraData> _playerCameraRepository;

        public T GetCameraDataByType<T>() where T : PlayerCameraData
        {
            return (T)_playerCameraRepository.FirstOrDefault(x => x is T);
        }
    }
}
