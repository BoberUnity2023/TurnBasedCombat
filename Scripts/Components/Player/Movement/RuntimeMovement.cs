using System;
using Anthill.Inject;
using Components.Interactable.Interface;
using Components.InteractablePicker;
using Pathfinding;
using UnityEngine;

namespace Components.Player.Movement
{
    public class RuntimeMovement : IPlayerMovement
    {
        private readonly PathMovement _pathMovement;
        
        private readonly CharacterController _player;
        private readonly CharacterRotation _characterRotation;

        private bool _isCanExecuted;
        private bool _isPathCompleted;
        private Action _pathCompleted;

        private Vector3 _lastPosition;
        
        private readonly float _minMoveDistance;
        private readonly InteractWithInteractionObjects _interactWithInteractionObjects;
        
        [Inject] public PositionPicker PositionPicker { get; set; }
        [Inject] public InteractablePicker.InteractablePicker InteractablePicker { get; set; }

        public RuntimeMovement(Seeker seeker, CharacterController player, CharacterRotation characterRotation)
        {
            AntInject.Inject(this);
            _player = player;
            _characterRotation = characterRotation;
            _pathMovement = new PathMovement(6, seeker, player.transform, 0.01f, 1f, 1f);
            _minMoveDistance = 0.75f;
            _interactWithInteractionObjects = new InteractWithInteractionObjects(_player.transform, _characterRotation);
        }

        public void AddHandlers()
        {
            PositionPicker.AddRayFindCallback(PathFindingHandler);
            InteractablePicker.AddInteractiveObjectWillFind(PathFindingHandler);
            _pathMovement.AddPathCompletedCallback(PathCompletedHandler);
        }

        public void RemoveHandlers()
        {
            PositionPicker.RemoveRayFindCallback(PathFindingHandler);
            InteractablePicker.RemoveInteractiveObjectWillFind(PathFindingHandler);
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

        public void Start()
        {
            _lastPosition = _player.transform.position;
            _isCanExecuted = true;
            _pathMovement.Start();
            _isPathCompleted = true;
        }

        public void Stop()
        {
            _isCanExecuted = false;
            _pathMovement.Stop();
        }

        public void Execute()
        {
            if (!_isCanExecuted || _isPathCompleted) return;

            _player.SimpleMove(_pathMovement.Velocity);
            _pathMovement.Execute();
            _characterRotation.SetTargetPointToRotate(_pathMovement.LastPointPosition);
        }

        private void PathCompletedHandler()
        {
            _isPathCompleted = true;
            Debug.Log("PathCompleted");
            _interactWithInteractionObjects.TryUse();
            _pathCompleted?.Invoke();
            _characterRotation.Stop();
        }
       
        private void PathFindingHandler(Vector3 position, IInteractable interactable)
        {
            if(!_isCanExecuted) return;
            
            Debug.Log(position);
            Debug.Log(_player.transform.position);
            _pathMovement.SetTargetDistance(.5f);
            _characterRotation.Start();
            _interactWithInteractionObjects.SetInteractableObject(interactable);
            _pathMovement.RecalculatePath(position);
            _isPathCompleted = false;
        }
        
        private void PathFindingHandler(Vector3 position)
        {
            if(!_isCanExecuted) return;
            
            if(Vector3.Distance(position, _lastPosition) < _minMoveDistance) return;
            _pathMovement.SetTargetDistance(0.01f);
            _lastPosition = position;
            _characterRotation.Start();
            _isPathCompleted = false;
            _pathMovement.RecalculatePath(position);
        }
    }
}