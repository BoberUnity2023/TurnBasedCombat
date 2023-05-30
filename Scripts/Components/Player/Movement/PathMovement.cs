using System;
using Pathfinding;
using UnityEngine;

namespace Components.Player.Movement
{
    public class PathMovement
    {
        private Path _path;

        private readonly Seeker _seeker;
        private readonly Transform _owner;

        private readonly float _nextWaypointDistance;
        private readonly float _repathRate;
        private readonly float _speed;

        private float _lastRepath = float.NegativeInfinity;

        private int _currentWaypoint = 0;

        private bool _reachedEndOfPath;
        private bool _isPathComplete;
        private bool _isCanExecute;

        private Vector3 _targetMovePosition;
        private Vector3 _lastPointPosition;
        private Vector3 _velocity;

        private Action _pathReleased;
        private Action _pathCompleted;

        private float _triggerDistance;


        public Vector3 LastPointPosition => _lastPointPosition;
        public Vector3 Velocity => _velocity;


        public PathMovement(float speed, Seeker seeker, Transform owner, float triggerDistance,
            float nextWaypointDistance, float repathRate)
        {
            _speed = speed;
            _seeker = seeker;
            _owner = owner;
            _triggerDistance = triggerDistance;
            _nextWaypointDistance = nextWaypointDistance;
            _repathRate = repathRate;
        }

        public void SetTargetDistance(float value)
        {
            _triggerDistance = value;
        }

        public void Start()
        {
            _isCanExecute = true;
            _targetMovePosition = _owner.transform.position;
            _isPathComplete = true;
        }

        public void Stop()
        {
            _isCanExecute = false;
        }

        public void AddPathReleasedCallback(Action callback)
        {
            _pathReleased += callback;
        }

        public void RemovePathReleasedCallback(Action callback)
        {
            _pathReleased -= callback;
        }

        public void AddPathCompletedCallback(Action callback)
        {
            _pathCompleted += callback;
        }

        public void RemovePathCompletedCallback(Action callback)
        {
            _pathCompleted -= callback;
        }

        public void RecalculatePath(Vector3 targetMovePosition)
        {
            _targetMovePosition = targetMovePosition;

            _isPathComplete = false;

            RecalculatePath();
        }

        private void PathCompleteHandler(Path p)
        {
            p.Claim(this);

            if (!p.error)
            {
                if (_path != null)
                    _path.Release(this);

                _path = p;
                _currentWaypoint = 0;
            }
            else
            {
                p.Release(this);
                _pathReleased?.Invoke();
            }
        }

        private void RecalculatePath()
        {
            _seeker.StartPath(_owner.position, _targetMovePosition, PathCompleteHandler);
        }
        
        public void Execute()
        {
            if (!_isCanExecute || _isPathComplete) return;

            var targetMove = new Vector3(_targetMovePosition.x, _owner.position.y, _targetMovePosition.z);
            var dist = (targetMove - _owner.position).sqrMagnitude;
            if (dist < _triggerDistance)
            {
                _isPathComplete = true;
                _velocity = Vector3.zero;
                _pathCompleted?.Invoke();
                return;
            }

            if (Time.time > _lastRepath + _repathRate && _seeker.IsDone())
            {
                _lastRepath = Time.time;
                RecalculatePath();
            }

            if (ReferenceEquals(_path, null)) return;

            var distanceToWaypoint = (_owner.position - _path.vectorPath[_currentWaypoint]).sqrMagnitude;
            if (distanceToWaypoint < _nextWaypointDistance)
            {
                if (_currentWaypoint >= _path.vectorPath.Count - 1)
                {
                    _reachedEndOfPath = true;
                }
                else
                {
                    _currentWaypoint++;
                }
            }

            var direction = (_path.vectorPath[_currentWaypoint] - _owner.position).normalized;

            _lastPointPosition = _path.vectorPath[_currentWaypoint];
            _velocity = direction * _speed;
        }
    }
}