using Cinemachine;

namespace Components.Camera.PlayerCamera.States
{
    public abstract class BaseCameraState
    {
        protected readonly CinemachineVirtualCamera _camera;
        
        protected BaseCameraState(CinemachineVirtualCamera camera)
        {
            _camera = camera;
        }

        public abstract void Execute();
        public abstract void Start();
        public abstract void Stop();
    }
}