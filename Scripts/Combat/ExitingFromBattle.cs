using System;
using UnityEngine;

namespace Combat
{
    public class ExitingFromBattle
    {
        private readonly PlayerCombatSystem _playerCombatSystem;
        private Vector3 _battleBeginPosition;

        private readonly float _battleExitingDistance;

        private Action _exiting;
        
        public ExitingFromBattle(PlayerCombatSystem playerCombatSystem, float battleExitingDistance)
        {
            _playerCombatSystem = playerCombatSystem;
            _battleExitingDistance = battleExitingDistance;
        }
        
        private bool _isExiting => Vector3.Distance(_battleBeginPosition,
            _playerCombatSystem.transform.position) > _battleExitingDistance;
        
        public void SetBattleBeginPosition(Vector3 battleBeginPosition)
        {
            _battleBeginPosition = battleBeginPosition;
        }

        public bool CheckExitingTheBattle()
        {
            if (_isExiting)
            {
                _exiting?.Invoke();
                return true;
            }

            return false;
        }

        public void AddExitingHandlers(Action callback)
        {
            _exiting += callback;
        }

        public void RemoveExitingHandlers(Action callback)
        {
            _exiting -= callback;
        }
        
       


    }
}