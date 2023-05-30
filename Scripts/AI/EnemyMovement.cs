using System;
using Anthill.Inject;
using Components.Player.Movement;
using Pathfinding;
using Stats;
using UnityEngine;

namespace AI
{
    public class EnemyMovement
    {
        private readonly PathMovement _pathMovement;
        private readonly CharacterController _enemy;
        private readonly CharacterRotation _characterRotation;

        private bool _isCanExecute;
        private bool _isPathComplete;
        
        private Action _complete;
        
        public EnemyMovement(Seeker seeker, CharacterController enemy, CharacterRotation characterRotation)
        {
            AntInject.Inject(this);
            _enemy = enemy;
            _characterRotation = characterRotation;
            _pathMovement = new PathMovement(6, seeker, enemy.transform, 1f, 1f, 0.75f);
            _isCanExecute = false;
        }

        public void AddHandlers()
        {
            _pathMovement.AddPathCompletedCallback(PathCompletedHandler);
        }

        public void RemoveHandlers()
        {
            _pathMovement.RemovePathCompletedCallback(PathCompletedHandler);
            _complete = null;
        }

        public void Start()
        {
            _isCanExecute = true;
            _pathMovement.Start();
            _isPathComplete = true;
        }

        public void Stop()
        {
            _isCanExecute = false;
            _pathMovement.Stop();
            _isPathComplete = true;
        }

        public void Execute()
        {
            if (!_isCanExecute || _isPathComplete) return;
            
            _pathMovement.Execute();
            
            _characterRotation.SetTargetPointToRotate(_pathMovement.LastPointPosition);
            _enemy.SimpleMove(_pathMovement.Velocity);
        }

        private void PathCompletedHandler()
        {
            _complete?.Invoke();
            Stop();
        }

        public void TargetFind(float distance, Transform target, Action complete)
        {
            _characterRotation.Start();
            
            _isPathComplete = false;
            
            _pathMovement.RecalculatePath(LerpByDistance(_enemy.transform.position, target.transform.position, distance));
            
            _complete = complete;
        }
        
        private Vector3 LerpByDistance(Vector3 A, Vector3 B, float length)
        {
            var fullLength= Vector3.Distance(A, B);
            var distance = A + (B - A) * (length / fullLength);
            return distance;
        }
    }
}