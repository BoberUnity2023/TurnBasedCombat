using Anthill.Inject;
using Cinemachine;
using Components.Camera.PlayerCamera.Interface;

namespace Components.Camera.PlayerCamera.CameraMoving
{
    public class MovingCameraBehaviourAround : IMovingCameraBehaviour
    {
        private readonly CinemachineOrbitalTransposer _orbitalTransposer;

        public MovingCameraBehaviourAround(CinemachineOrbitalTransposer orbital)
        {
            AntInject.Inject(this);
            _orbitalTransposer = orbital;
        }

        public void Execute()
        {
            
        }

        public void StartRotate()
        {
            _orbitalTransposer.m_XAxis.m_MaxSpeed = 360f;
        }

        public void StopRotate()
        {
            _orbitalTransposer.m_XAxis.m_MaxSpeed = 0f;
        }
    }
}