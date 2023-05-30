using System;
using System.Collections.Generic;
using Anthill.Inject;
using Components.Grid;
using Components.InteractablePicker;
using Pathfinding;
using Stats.Side;
using UnityEngine;

namespace Components.Player.Movement
{
    public class TurnBaseMovement : IPlayerMovement
    {
        private readonly PathMovement _pathMovement;
        
        private readonly CharacterRotation _characterRotation;
        
        private readonly CharacterController _player;
        
        private readonly MoveActionPoints _moveActionPoints;
        
        private readonly Seeker _seeker;

        private readonly LayerMask _layerMask;

        private GridSegment _tempGrid;
        private GraphNode _targetNode;
        private GridSegmentGenerator _gridSegmentGenerator;
     
        private bool _isCanExecuted;
        private bool _isPathCompleted;
        
        private Action _pathCompleted;
        
        [Inject] public GridSegmentPicker GridSegmentPicker { get; set; }
        

        public TurnBaseMovement(float speed, CharacterController player, MoveActionPoints moveActionPoints,
            CharacterRotation characterRotation, Seeker seeker, GridSegmentGenerator gridSegmentGenerator)
        {
            AntInject.Inject(this);
        
            _player = player;
            _moveActionPoints = moveActionPoints;
       
            _seeker = seeker;

            _pathMovement = new PathMovement(speed, _seeker, _player.transform, 0.001f, 0.15f, 3f);

            _gridSegmentGenerator = gridSegmentGenerator;

            _characterRotation = characterRotation;

            _layerMask.value = LayerMask.GetMask("Grid") * -1;
        }

        public void Start()
        {
            _isCanExecuted = true;
            
            _pathMovement.Start();
            _seeker.startEndModifier.exactStartPoint = StartEndModifier.Exactness.SnapToNode;
            _seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.SnapToNode;
            _isPathCompleted = true;
        }

        public void Stop()
        {
            _isCanExecuted = false;
            _pathMovement.Stop();
            _seeker.startEndModifier.exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;
            _seeker.startEndModifier.exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;
        }

        
        public void AddHandlers()
        {
            GridSegmentPicker.AddGridSegmentFindCallback(PathFindHandler);
            _pathMovement.AddPathCompletedCallback(PathCompletedHandler);
        }

        public void RemoveHandlers()
        {
            GridSegmentPicker.RemoveGridSegmentFindCallback(PathFindHandler);
            _pathMovement.RemovePathCompletedCallback(PathCompletedHandler);
        }

        public void AddPathCompletedCallback(Action callback)
        {
            _pathCompleted += callback;
        }

        public void RemovePathCompletedCallback(Action callback)
        {
            _pathCompleted -= callback;
        }

        public void GenerateGrid()
        {
            _gridSegmentGenerator.GenerateGrid((int)_moveActionPoints.Value);
        }

        public void DestroyGrid()
        {
            _gridSegmentGenerator.DestroyGrid();
        }

        public void DestroyPath()
        {
            _gridSegmentGenerator.DestroyPath();

        }
        
        public void Execute()
        {
            if (!_isCanExecuted || _isPathCompleted) return;

            _pathMovement.Execute();
            _characterRotation.SetTargetPointToRotate(_pathMovement.LastPointPosition);
            _player.SimpleMove(_pathMovement.Velocity);
        }

        private void PathCompletedHandler()
        {
            _pathCompleted?.Invoke();
            _isPathCompleted = true;
            GenerateGrid();
        }

        private void PathFindHandler(GridSegment gridSegment)
        {
            if(!_isCanExecuted) return;
            
            if(ReferenceEquals(gridSegment, null)) return;
            
            _tempGrid = gridSegment;
            
            
            var targetPosition = (Vector3)_tempGrid.GraphNode.position;
            _pathMovement.RecalculatePath(targetPosition);
            _isPathCompleted = false;
            DestroyGrid();
            var distance = Vector3.Distance(_player.transform.position,
                new Vector3(targetPosition.x, _player.transform.position.y, targetPosition.z));
            
            _moveActionPoints.Reduce((int)distance + 1);
        }
    }
    
    
}