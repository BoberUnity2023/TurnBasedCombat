using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Components.Camera.PlayerCamera.Interface;
using Components.Camera.PlayerCamera.States;
using Data.CameraData.PlayerCamera;

namespace Components.Camera.PlayerCamera
{
    public class CameraBehaviour : ICameraStateSwitcher
    {
        private BaseCameraState _currentState;
        private readonly List<BaseCameraState> _allStates;
        public bool IsFreeLockState => _currentState is FreeLockState;

        public CameraBehaviour(CinemachineVirtualCamera linkedCamera, CinemachineVirtualCamera freeLockCamera, ICameraTarget target , PlayerCameraDataRepository dataRepository)
        {
            _allStates = new List<BaseCameraState>()
            {
                new LinkedCameraState(linkedCamera, dataRepository.GetCameraDataByType<LinkedCameraData>()),
                new FreeLockState(freeLockCamera, target, dataRepository.GetCameraDataByType<FreeCameraData>())
            };
            
            _currentState = _allStates[0];
            _currentState.Start();
        }

        public void SwitchState<T>() where T : BaseCameraState
        {
            var state = _allStates.FirstOrDefault(x => x is T);
            _currentState.Stop();
            state.Start();
            _currentState = state;
        }

        public void Execute()
        {
            _currentState.Execute();
        }
    }
}