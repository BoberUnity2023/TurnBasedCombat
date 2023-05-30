using System.Collections.Generic;

namespace Extensions.Transform
{
    public static class TransformExtensions
    {
        public static UnityEngine.Transform FindNearPoint(this UnityEngine.Transform target, List<UnityEngine.Transform> objects)
        {
            var nearPoint = objects[0];
            if (nearPoint == null) return null;
            for (var i = 1; i < objects.Count; i++)
            {
                if (objects[i] == null) continue;

                var point = objects[i];
                var distanceNext = (target.position - point.position).sqrMagnitude;
                var distanceCurrent = (target.position - nearPoint.position).sqrMagnitude;

                if (distanceCurrent > distanceNext)
                {
                    nearPoint = point;
                }
            }

            return nearPoint.transform;
        }
    }
}