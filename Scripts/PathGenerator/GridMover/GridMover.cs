using System.Diagnostics;
using Components.Player;
using Pathfinding;
using UnityEngine;

namespace PathGenerator.GridMover
{
    public class GridMover : MonoBehaviour
    {
        [SerializeField] private float _repathRate = 2f;

        private Player _player;
        private LayerGridGraph _layerGrid;
        private float _lastRepath = float.NegativeInfinity;


        private Stopwatch _stopwatch = new Stopwatch();

        private bool _isCanMove;
        public Player Player => _player;


        public void Scan()
        {
            _layerGrid.Scan();
        }
    
    
        public void Init(Player player)
        {
            _player = player;
            _layerGrid = AstarPath.active.data.layerGridGraph;
        }

        public void StopMove()
        {
            _isCanMove = false;
        }

        public void StartMove()
        {
            _isCanMove = true;
        }

        public void Execute()
        {
            if (!_isCanMove) return;
            if (Time.time > _lastRepath + _repathRate)
            {
                _lastRepath = Time.time;
                UpdateGraph();
            }
        }

        private void UpdateGraph()
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            Vector3 dir = PointToGraphSpace(_player.transform.position) - PointToGraphSpace(_layerGrid.center);

            dir.x = Mathf.Round(dir.x);
            dir.z = Mathf.Round(dir.z);
            dir.y = 0;


            _layerGrid.center += _layerGrid.transform.TransformVector(dir);
            _layerGrid.UpdateTransform();

            Scan();

            _stopwatch.Stop();
        }


        private Vector3 PointToGraphSpace(Vector3 p)
        {
            return _layerGrid.transform.InverseTransform(p);
        }
    }
}