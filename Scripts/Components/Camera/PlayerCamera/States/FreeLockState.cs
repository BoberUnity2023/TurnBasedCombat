using Cinemachine;
using Components.Camera.PlayerCamera.CameraMoving;
using Components.Camera.PlayerCamera.Interface;
using Data.CameraData.PlayerCamera;
using UnityEngine;

namespace Components.Camera.PlayerCamera.States
{
    public class FreeLockState : BaseCameraState
    {
        private readonly IMovingCameraBehaviour _freeMovementBehaviour;
        private readonly FreeLockScrollMovement _scroll;
        private readonly ICameraTarget _cameraTarget;

        private readonly float _zOffset;

        public FreeLockState(CinemachineVirtualCamera camera, ICameraTarget cameraTarget, FreeCameraData data) : base(camera)
        {
            _cameraTarget = cameraTarget;
            _zOffset = data.StartOffset;
            _freeMovementBehaviour = new FreeMovementBehaviour(data.SpeedMove, data.EdgeStep, data.KeyboardStep,
                data.MaxOffset, data.MaxOffset, _zOffset, camera, _cameraTarget);
            _scroll = new FreeLockScrollMovement(camera.transform, data.ScrollSpeed, data.SmoothScrollSpeed,
                data.ScrollY);
        }

        public override void Execute()
        {
            _freeMovementBehaviour.Execute();
            _scroll.Execute();
        }

        public override void Start()
        {
            MoveCameraOnStartPosition();
            _camera.gameObject.SetActive(true);
        }

        public override void Stop()
        {
            _camera.gameObject.SetActive(false);
        }

        private void MoveCameraOnStartPosition()
        {
            _camera.transform.position = new Vector3(_cameraTarget.Target.position.x, _camera.transform.position.y, _cameraTarget.Target.position.z - _zOffset);
            _camera.transform.LookAt(_cameraTarget.Target);
        }
    }
}