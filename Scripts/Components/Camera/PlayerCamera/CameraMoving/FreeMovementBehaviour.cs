using System;
using System.Collections.Generic;
using Anthill.Inject;
using Cinemachine;
using Components.Camera.PlayerCamera.Interface;
using InputControl;
using UnityEngine;

namespace Components.Camera.PlayerCamera.CameraMoving
{
    public class FreeMovementBehaviour : IMovingCameraBehaviour
    {
        private class FreeMovementRules
        {
            private readonly Func<Vector3> _action;
            private readonly Func<bool> _isRuleExecute;

            public bool IsRuleExecute => _isRuleExecute();

            public FreeMovementRules(Func<bool> isRuleExecute, Func<Vector3> action)
            {
                _isRuleExecute = isRuleExecute;
                _action = action;
            }

            public Vector3 ExecuteAction()
            {
                return _action();
            }
        }

        private readonly float _speed;
        private readonly float _xOffset;
        private readonly float _zOffset;
        private readonly float _startZOffset;

        private readonly CinemachineVirtualCamera _camera;

        private readonly Transform _point;

        private readonly List<FreeMovementRules> _rules;

        private readonly ICameraTarget _cameraTarget;

        private bool _isMoveToStartPosition;

        private Vector3 _target;

        [Inject] public ServiceInput ServiceInput { get; set; }


        public FreeMovementBehaviour(float speed, float edgeStep, float keyboardStep, float xOffset, float zOffset,
            float startZOffset, CinemachineVirtualCamera camera, ICameraTarget cameraTarget)
        {
            AntInject.Inject(this);
            _speed = speed;

            _xOffset = xOffset;
            _zOffset = zOffset;
            _startZOffset = startZOffset;
            _camera = camera;
            _cameraTarget = cameraTarget;
            _point = cameraTarget.Target;

            _rules = new List<FreeMovementRules>()
            {
                new FreeMovementRules(
                    () => ServiceInput.CameraInput.MousePositionVertical.Value >= Screen.height * 0.97f,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.forward), edgeStep, 1)),
                new FreeMovementRules(
                    () => ServiceInput.CameraInput.MousePositionVertical.Value <= Screen.height * 0.03f,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.forward), edgeStep, -1)),
                new FreeMovementRules(
                    () => ServiceInput.CameraInput.MousePositionHorizontal.Value >= Screen.width * 0.97f,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.right), edgeStep, 1)),
                new FreeMovementRules(
                    () => ServiceInput.CameraInput.MousePositionHorizontal.Value <= Screen.width * 0.03f,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.right), edgeStep, -1)),

                new FreeMovementRules(() => ServiceInput.KeyboardInput.Vertical.Value > 0,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.up), keyboardStep, 1)),
                new FreeMovementRules(() => ServiceInput.KeyboardInput.Vertical.Value < 0,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.up), keyboardStep, -1)),
                new FreeMovementRules(() => ServiceInput.KeyboardInput.Horizontal.Value < 0,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.right), keyboardStep, -1)),
                new FreeMovementRules(() => ServiceInput.KeyboardInput.Horizontal.Value > 0,
                    () => MoveCameraByCoordinate(GetNormalizedVector(_camera.transform.right), keyboardStep, 1)),
            };
        }


        public void Execute()
        {
            var nextPosition = _camera.transform.position;

            foreach (var rule in _rules)
            {
                if (rule.IsRuleExecute)
                {
                    nextPosition += rule.ExecuteAction();
                }
            }


            if (ServiceInput.CameraInput.MoveOnStartPositionButton.IsPressed)
            {
                _isMoveToStartPosition = true;
            }

            if (_isMoveToStartPosition)
            {
                _target = new Vector3(_cameraTarget.Target.position.x, _camera.transform.position.y,
                    _cameraTarget.Target.position.z - _startZOffset);

                var distance = Vector3.Distance(_camera.transform.position,
                    _target);
                if (distance > 0.1f)
                {
                    var towardX = Mathf.MoveTowards(_camera.transform.position.x, _target.x, _speed * Time.deltaTime);
                    var towardZ = Mathf.MoveTowards(_camera.transform.position.z, _target.z, _speed * Time.deltaTime);

                    _camera.transform.position = new Vector3(towardX, _camera.transform.position.y, towardZ);


                    return;
                }


                _isMoveToStartPosition = false;
            }


            nextPosition = ClampVector(nextPosition);

            var position = _camera.transform.position;
            position = Vector3.MoveTowards(position, new Vector3(nextPosition.x, position.y, nextPosition.z),_speed * Time.deltaTime);
            _camera.transform.position = position;
        }

        private Vector3 ClampVector(Vector3 target)
        {
            return new Vector3(Math.Clamp(target.x, _point.position.x - _xOffset, _point.position.x + _xOffset),
                0,
                Math.Clamp(target.z, _point.position.z - _zOffset - _startZOffset,
                    _point.position.z + _zOffset - _startZOffset));
        }

        private Vector3 MoveCameraByCoordinate(Vector3 direction, float step, int isLeft)
        {
            return direction * (step * isLeft * Time.deltaTime);
        }

        private Vector3 GetNormalizedVector(Vector3 rotation)
        {
            rotation.y = 0;
            return rotation.normalized;
        }
    }
}