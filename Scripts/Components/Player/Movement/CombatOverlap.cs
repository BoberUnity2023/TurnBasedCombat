using System;
using System.Collections.Generic;
using AI;
using Anthill.Inject;
using GameState;
using UnityEngine;

namespace Components.Player.Movement
{
    public class CombatOverlap
    {
        private readonly float _radius;
        private readonly Transform _target;

        private readonly LayerMask _layerMask;

        private bool _isCanExecute;

        private readonly float _repathRate = 0.5f;
        
        private float _lastRepath = float.NegativeInfinity;

        private Action<List<EnemyAI>> _targetFinding;
        private Action _groupCompleted;


        public CombatOverlap(float radius, Transform target)
        {
            AntInject.Inject(this);
            _radius = radius;
            _target = target;

            _layerMask = LayerMask.GetMask("ICanFight") * -1;
        }

        public void Execute()
        {
            if (!(Time.time > _lastRepath + _repathRate) || !_isCanExecute) return;

            _lastRepath = Time.time;

            FindTarget();
        }

        public void AddHandlers(Action<List<EnemyAI>> callback, Action groupCompleted)
        {
            _targetFinding += callback;
            _groupCompleted += groupCompleted;
        }

        public void RemoveHandlers(Action<List<EnemyAI>> callback, Action groupCompleted)
        {
            _targetFinding -= callback;
            _groupCompleted -= groupCompleted;
        }

        private void StopExecute()
        {
            _isCanExecute = false;
        }

        public void StartExecute()
        {
            _isCanExecute = true;
        }
        

        private void FindTarget() 
        {
            var colliders = Physics.OverlapSphere(_target.position, _radius, _layerMask);


            var targets = new List<EnemyAI>(); 
            
            foreach (var item in colliders)
            {
                if (item.TryGetComponent<EnemyAI>(out EnemyAI component))
                {
                    targets.Add(component);
                }
            }

            if (targets.Count >= 1)
            {
                var original = targets[0];
                for (int i = 0; i < targets.Count; i++)
                {
                    var item = targets[i];
                    if (!ReferenceEquals(original.CombatGroup, item.CombatGroup))
                    {
                        _targetFinding?.Invoke(item.CombatGroup.EnemyAis);
                    } 
                }
                _targetFinding?.Invoke(original.CombatGroup.EnemyAis);
                _groupCompleted?.Invoke();
                StopExecute();
            }
        }
    }
}