using Anthill.Inject;
using Cinemachine;
using Components.Camera.PlayerCamera.Interface;
using Data.CameraData.PlayerCamera;
using InputControl;
using UnityEngine;

namespace Components.Camera.PlayerCamera.CameraMoving
{
    public class ScrollMovementBehaviour : IMovingCameraBehaviour
    {
        private readonly Transform _camera;
        private readonly CinemachineTransposer _transposer;
        private readonly float _stepOffset;
      
        private readonly ModelScroll _scrollY;
        private readonly ModelScroll _scrollZ;

        private Vector3 _targetDistance;
        private readonly Vector3 _direction;

        private readonly float _durationMove;
        private readonly float _averageDistanceY;
        private readonly float _averageDistanceZ;
        private bool _isScrollMovement;


        [Inject] public ServiceInput ServiceInput { get; set; }

        public ScrollMovementBehaviour(CinemachineOrbitalTransposer transposer, float stepOffset, float durationMove, ModelScroll scrollY, ModelScroll scrollZ)
        {
            AntInject.Inject(this);

            _transposer = transposer;
            _stepOffset = stepOffset;
            _durationMove = durationMove;
            _scrollY = scrollY;
            _scrollZ = scrollZ;

            _averageDistanceY = (scrollY.ScrollMaxDistance + scrollY.ScrollMinDistance) / 2f;
            _averageDistanceZ = (scrollZ.ScrollMaxDistance + scrollZ.ScrollMinDistance) / 2f;

            _direction = transposer.m_FollowOffset.normalized;
        }

        public void Execute()
        {
            var zoomOffset = _direction * (ServiceInput.CameraInput.Zoom.Value * _stepOffset);
            UpdateOffset(zoomOffset);

            if (ServiceInput.CameraInput.MoveOnStartPositionButton.IsPressed)
            {
                _targetDistance = new Vector3(0, _averageDistanceY, _averageDistanceZ);
            }

            UpdateZoom();
        }

        private void UpdateOffset(Vector3 offset)
        {
            if (offset == Vector3.zero) return;
            var target = _transposer.m_FollowOffset + offset;

            _targetDistance = ClampedOffset(target);
        }

        private Vector3 ClampedOffset(Vector3 target)
        {
            var yClamp = Mathf.Clamp(target.y, _scrollY.ScrollMinDistance, _scrollY.ScrollMaxDistance);
            var zClamp = Mathf.Clamp(target.z, _scrollZ.ScrollMinDistance, _scrollZ.ScrollMaxDistance);


            return new Vector3(0, yClamp, zClamp);
        }

        private void UpdateZoom()
        {
            if (_targetDistance == Vector3.zero) return;

            var distance = Vector3.Distance(_transposer.m_FollowOffset, _targetDistance);
            
            if (distance > 0.1f)
            {
                _transposer.m_FollowOffset =
                    Vector3.MoveTowards(_transposer.m_FollowOffset, _targetDistance, _durationMove * Time.deltaTime);

                return;
            }

            _targetDistance = Vector3.zero;
        }
    }
}