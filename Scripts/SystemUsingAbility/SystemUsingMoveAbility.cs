using System;
using System.Collections.Generic;
using Abilities.Active;
using Components.Grid;
using Components.Player.Movement;
using Stats;
using Stats.Side;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace SystemUsingAbility
{
    public class SystemUsingMoveAbility : ISystemUsingAbility<PlayerMoveAbility>
    {
        private readonly ActionPoints _actionPoints;
        private readonly MoveActionPoints _moveActionPoints;
        private readonly TurnBaseMovement _turnBaseMovement;
        private readonly GridSegmentGenerator _gridSegmentGenerator;
        
        private Action _abilityCompleted;
        
        public SystemUsingMoveAbility(MoveActionPoints moveActionPoints, TurnBaseMovement turnBaseMovement, GridSegmentGenerator gridSegmentGenerator)
        {
            _moveActionPoints = moveActionPoints;
            _turnBaseMovement = turnBaseMovement;
            _gridSegmentGenerator = gridSegmentGenerator;

        }

        public void AddHandlers()
        {
            _turnBaseMovement.AddPathCompletedCallback(AbilityCompletedHandler);
        }

        public void RemoveHandlers()
        {
            _turnBaseMovement.RemovePathCompletedCallback(AbilityCompletedHandler);
        }

        public TK ChangeCurrentAbility<TK>(TK ability) where TK : PlayerMoveAbility
        {
            return null;
        }

        public TK ChangeCurrentAbility<TK>() where TK : PlayerMoveAbility
        {
            return null;
        }

        public void CreateAbilities(List<PlayerMoveAbility> original)
        {
            
        }

        public void AbilityCompletedHandler()
        {
            if ( _moveActionPoints.Value < 1)
            {
                // _abilityCompleted?.Invoke();
            }
        }
        
        public void AddAbilityCompleteHandler(Action abilityCompleted)
        {
            _abilityCompleted += abilityCompleted;
        }
        
        public void RemoveAbilityCompleteHandler(Action abilityCompleted)
        {
            _abilityCompleted -= abilityCompleted;
        }

        public PlayerMoveAbility GetAbility()
        {
            return null;
        }

        public void Start()
        {
            _gridSegmentGenerator.DestroyGrid();
            _turnBaseMovement.GenerateGrid();
        }

        public void Stop()
        {
            _turnBaseMovement.DestroyGrid();
        }

        public void GenerateOutline()
        {
           
        }

        public void RemoveOutline()
        {
            _gridSegmentGenerator.DestroyPath();
        }

        public void RoundEnd()
        {
            _gridSegmentGenerator.DestroyGrid();
            _turnBaseMovement.GenerateGrid();
        }
    }
}