using System.Collections.Generic;
using System.Linq;
using Anthill.Inject;
using Libraries.DNV.DNVPool.Interface;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using Pathfinding;
using UnityEngine;

namespace Components.Grid
{
    public class GridSegmentGenerator
    {
        private readonly Transform _target;
        private readonly BlockManager.TraversalProvider _traversalProvider;

        private readonly Pool<GridSegment> _activeSegments;
        private readonly LineRenderer _lineRenderer;
        private readonly List<IPoolObject> _gridSegments;

        private Vector3[] _vertices;
        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

        public GridSegmentGenerator(Transform target, BlockManager.TraversalProvider traversalProvider,
            LineRenderer lineRenderer)
        {
            AntInject.Inject(this);
            _lineRenderer = lineRenderer;
            _activeSegments = ObjectPoolContainer.GetPool<GridSegment>();
            _target = target;
            _traversalProvider = traversalProvider;
            _gridSegments = new List<IPoolObject>();
        }

        public void GenerateGrid(int count)
        {
            var path = GeneratePath(count);
            foreach (var node in path.allNodes)
            {
                if (node != path.startNode)
                {
                    var item = _activeSegments.GetItem();

                    item.SetPosition((Vector3)node.position);
                    _gridSegments.Add(item);
                    item.GraphNode = node;
                }
            }
        }

        public void DestroyGrid()
        {
            for (int i = 0; i < _gridSegments.Count; i++)
            {
                _gridSegments[i].ReturnToPool();
            }

            _gridSegments.Clear();
        }

        public void DestroyPath()
        {
            _lineRenderer.positionCount = 0;
        }

        private ConstantPath GeneratePath(int count)
        {
            var path = ConstantPath.Construct(_target.transform.position,
                count * 1000 + 1);
            path.traversalProvider = _traversalProvider;
            AstarPath.StartPath(path);
            path.BlockUntilCalculated();
            CreateOutline(path);

            return path;
        }

        private void CreateOutline(ConstantPath path)
        {
            GridNodeBase[] gridNodes = path.allNodes.OfType<GridNodeBase>().ToArray();
            var grid = AstarPath.active.data.layerGridGraph;

            GraphUtilities.GetContours(grid, vertices =>
            {
                if (vertices.Length > 4)
                {
                    _vertices = vertices;
                }
            }, 0, gridNodes);

            if (gridNodes.Length > 4)
            {
                _lineRenderer.positionCount = _vertices.Length;
                for (int i = 0; i < _vertices.Length; i++)
                {
                    _lineRenderer.SetPosition(i, _vertices[i] + Vector3.up / 2f);
                }
            }
            else
            {
                DestroyPath();
            }
        }
    }
}