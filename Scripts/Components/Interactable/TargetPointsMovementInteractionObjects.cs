using System.Collections.Generic;
using Anthill.Inject;
using UnityEngine;

namespace Components.Interactable
{
    public class TargetPointsMovementInteractionObjects
    {
        private readonly List<Transform> _points;
        private readonly Transform _target;
        [Inject] public Player.Player Player { get; set; }

        public TargetPointsMovementInteractionObjects(List<Transform> points)
        {
            AntInject.Inject(this);
            _points = new List<Transform>(points);
            _target = Player.transform;
        }

        public Transform FindNearPointToMove()
        {
            var nearPoint = _points[0];
            for (var i = 1; i < _points.Count; i++)
            {
                var point = _points[i];
                var distanceNext = (_target.position - point.position).sqrMagnitude;
                var distanceCurrent = (_target.position - nearPoint.position).sqrMagnitude;

                if (distanceCurrent > distanceNext)
                {
                    nearPoint = point;
                }
            }

            return nearPoint;
        }
    }
}