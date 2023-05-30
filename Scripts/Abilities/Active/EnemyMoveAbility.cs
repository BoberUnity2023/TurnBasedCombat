using System;
using AI;
using Stats;
using UnityEngine;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "EnemyMoveAbility", menuName = "Ability/Active/EnemyMoveAbility")]
    public class EnemyMoveAbility : MoveAbility
    {
        [SerializeField] private int _moveDistance;
        
        private EnemyMovement _enemyMovement;
        private SideStats _sideStats;

        private Transform _target;
        private float _attackDistance;

        private Action _callback;
        
        
        public void Init(SideStats sideStats, EnemyMovement enemyMovement, Transform target, float attackDistance, Action callback)
        {
            _enemyMovement = enemyMovement;
            _sideStats = sideStats;
            _target = target;
            _attackDistance = attackDistance;
            _callback = callback;
        }

        public override void Move()
        {
            _enemyMovement.TargetFind(_moveDistance, _target, _callback);
        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {
            
        }
    }
}