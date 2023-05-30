using Anthill.Inject;
using Components.Camera.PlayerCamera.Interface;
using Data.CameraData.PlayerCamera;
using InputControl;
using UnityEngine;

namespace Components.Camera.PlayerCamera.CameraMoving
{
    public class FreeLockScrollMovement : IMovingCameraBehaviour
    {
        private readonly float _stepOffset;
        private readonly float _averageDistance;
        private readonly float _durationMove;

        private readonly ModelScroll _scrollY;
        private readonly Transform _cameraTarget;
        private readonly Transform _camera;

        private Vector3 _direction;
        private Vector3 _targetDistance;
        [Inject] public ServiceInput ServiceInput { get; set; }

        public FreeLockScrollMovement(Transform camera, float stepOffset, float durationMove, ModelScroll scrollY)
        {
            _camera = camera.transform;
            _stepOffset = stepOffset;
            _durationMove = durationMove;
            _scrollY = scrollY;
            _averageDistance = (scrollY.ScrollMaxDistance + scrollY.ScrollMinDistance) / 2;

            _direction = camera.transform.position.normalized;

            AntInject.Inject(this);
        }


        public void Execute()
        {
            var zoomOffset = _direction * (ServiceInput.CameraInput.Zoom.Value * _stepOffset);
            UpdateOffset(zoomOffset);

            if (ServiceInput.CameraInput.MoveOnStartPositionButton.IsPressed)
            {
                _targetDistance = new Vector3(0, _averageDistance, 0);
            }

            UpdateZoom();
        }


        private void UpdateZoom()
        {
            if (_targetDistance == Vector3.zero) return;

            var distance = Vector3.Distance(_camera.transform.position, _targetDistance);
            if (distance > 0.1f)
            {
                var towardY = Mathf.MoveTowards(_camera.transform.position.y, _targetDistance.y,
                    _durationMove * Time.deltaTime);
                _camera.transform.position =
                    new Vector3(_camera.transform.position.x, towardY, _camera.transform.position.z);


                return;
            }

            _targetDistance = Vector3.zero;
        }

        private void UpdateOffset(Vector3 offset)
        {
            if (offset == Vector3.zero) return;
            var target = _camera.transform.position + offset;

            _targetDistance = ClampedOffset(target);
        }

        private Vector3 ClampedOffset(Vector3 target)
        {
            var yClamp = Mathf.Clamp(target.y, _scrollY.ScrollMinDistance, _scrollY.ScrollMaxDistance);

            return new Vector3(0, yClamp, 0);
        }
    }
}