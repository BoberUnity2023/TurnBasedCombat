using System;
using Anthill.Inject;
using Cinemachine;
using Components.Camera.PlayerCamera.States;
using Data.CameraData.PlayerCamera;
using InputControl;
using UnityEngine;

namespace Components.Camera.PlayerCamera
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _linkedCamera;
        [SerializeField] private CinemachineVirtualCamera _freeLookCamera;
        [SerializeField] private CinemachineVirtualCamera _dialogCamera;

        [SerializeField] private PlayerCameraDataRepository _dataRepository;

        [Inject] public ServiceInput ServiceInput { get; set; }
        
        private CameraBehaviour _cameraBehaviour;

        private bool _isInited;

        public void Init(ICameraTarget player)
        {
            AntInject.Inject(this);
            _cameraBehaviour = new CameraBehaviour(_linkedCamera, _freeLookCamera, player, _dataRepository);
            _isInited = true;
            
            InstallCamerasTarget(player);
        }

        public void Execute()
        {
            if(!_isInited) return;

            if (ServiceInput.CameraInput.SwitchCameraStateButton.IsPressed)
            {
                if (_cameraBehaviour.IsFreeLockState)
                {
                    ChangeCameraType<LinkedCameraState>();
                }
                else
                {
                    ChangeCameraType<FreeLockState>();
                }
            }
            
            _cameraBehaviour.Execute();
        }

        private void ChangeCameraType<T>() where T : BaseCameraState
        {
            _cameraBehaviour.SwitchState<T>();
        }

        private void InstallCamerasTarget(ICameraTarget cameraTarget)
        {
            _linkedCamera.Follow = cameraTarget.Target;
            _linkedCamera.LookAt = cameraTarget.Target;
            _dialogCamera.LookAt = cameraTarget.Target;
        }
    }
}
