using Anthill.Inject;
using Cinemachine;
using Components.Camera.PlayerCamera.CameraMoving;
using Data.CameraData.PlayerCamera;
using InputControl;

namespace Components.Camera.PlayerCamera.States
{
    public class LinkedCameraState : BaseCameraState
    {
        private readonly ScrollMovementBehaviour _scrollMovementBehaviour;
        private readonly MovingCameraBehaviourAround _movingCameraBehaviourAround;

        [Inject] public ServiceInput ServiceInput { get; set; }

        public LinkedCameraState(CinemachineVirtualCamera camera, LinkedCameraData data) : base(camera)
        {
            AntInject.Inject(this);

            var cinemachineOrbitalTransposer = 
                camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            _scrollMovementBehaviour = new ScrollMovementBehaviour(
                cinemachineOrbitalTransposer,
                data.ScrollSpeed,
                data.SmoothScrollSpeed,
                data.ScrollY,
                data.ScrollZ);

            _movingCameraBehaviourAround = new MovingCameraBehaviourAround(cinemachineOrbitalTransposer);
        }

        public override void Execute()
        {
            if (ServiceInput.CameraInput.RightMouseButton.IsPressed)
            {
                _movingCameraBehaviourAround.StartRotate();
            }

            if (ServiceInput.CameraInput.RightMouseButton.IsReleased)
            {
                _movingCameraBehaviourAround.StopRotate();
            }

            _movingCameraBehaviourAround.Execute();
            _scrollMovementBehaviour.Execute();
        }

        public override void Start()
        {
            _camera.gameObject.SetActive(true);
        }

        public override void Stop()
        {
            _camera.gameObject.SetActive(false);
        }
    }
}