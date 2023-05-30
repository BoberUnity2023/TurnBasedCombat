using System;
using Abilities.Active;
using AI;
using Anthill.Inject;
using Components.Grid;
using Components.InteractablePicker;
using Components.Player.Movement;
using DamageAcquisition;
using Stats;
using SystemUsingAbility;
using Units;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(PlayerAbilitySystem), typeof(BattleMarker))]
    public class PlayerCombatSystem : MonoBehaviour
    {
        private PlayerAbilitySystem _playerAbilitySystem;
        private DamageAcquisitionSystem _damageAcquisitionSystem;
        private Action _combatActionsCompleted;
        private BattleMarker _battleMarker;
        private CharacterRotation _characterRotation;
        private bool _isCanExecute;

        public event Action BattleBeginEvent;
        public event Action BattleCompletedEvent;

        [Inject] public DamageablePicker DamageablePicker { get; set; }
        [Inject] public VisionGameUnit VisionGameUnit { get; set; }

        public void Init(SideStats sideStats, DamageAcquisitionSystem damageAcquisitionSystem, TurnBaseMovement turnBaseMovement, GridSegmentGenerator gridSegmentGenerator, CharacterRotation characterRotation)
        {
            AntInject.Inject(this);
            _damageAcquisitionSystem = damageAcquisitionSystem;
            _playerAbilitySystem = GetComponent<PlayerAbilitySystem>();

            _playerAbilitySystem.Init(sideStats, turnBaseMovement, gridSegmentGenerator, _damageAcquisitionSystem);
            
            _characterRotation = characterRotation;
            _battleMarker = GetComponent<BattleMarker>();

            AddHandlers();
        }

        public void AddCombatActionCompletedHandlers(Action combatActionsCompleted)
        {
            _combatActionsCompleted += combatActionsCompleted;
        }

        public void RemoveCombatActionCompletedHandlers(Action combatActionsCompleted)
        {
            _combatActionsCompleted -= combatActionsCompleted;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeActiveAbilityOnMoveAbility();
                Debug.Log($"KeyCode.Keypad3");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeMoveAbilityOnActiveAbility();
                _playerAbilitySystem.ChangeCurrentAbility<IceRicochet>();
                Debug.Log($"KeyCode.Keypad2");
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeMoveAbilityOnActiveAbility();
                _playerAbilitySystem.ChangeCurrentAbility<MeteoriteStrike>();
                Debug.Log($"KeyCode.Keypad1");
            }
        }

        public void RoundEnd()
        {
            _playerAbilitySystem.RoundEnd();
            _damageAcquisitionSystem.RoundEnd();
            _playerAbilitySystem.GenerateMoveDistanceOutline();
            ChangeActiveAbilityOnMoveAbility();
        }

        public void BattleBegin()
        {
            _isCanExecute = true;
            ChangeActiveAbilityOnMoveAbility();
            _battleMarker.Show();
            BattleBeginEvent?.Invoke();
        }

        public void BattleCompleted()
        {
            _isCanExecute = false;
            _playerAbilitySystem.BattleCompleted();
            _battleMarker.Hide();
            BattleCompletedEvent?.Invoke();
        }

        private void UseAbilitiesOverHandler()
        {
            _combatActionsCompleted?.Invoke();
        }

        private void ChangeActiveAbilityOnMoveAbility()
        {
            if(!_isCanExecute) return;

            _playerAbilitySystem.ChangeActiveAbilityOnMoveAbility();
        }

        private void ChangeMoveAbilityOnActiveAbility()
        {
            if(!_isCanExecute) return;

            _playerAbilitySystem.ChangeMoveAbilityOnActiveAbility();    
        }

        private void AddHandlers()
        {
            DamageablePicker.AddRayFindCallback(FindTarget);
            DamageablePicker.AddPointEnterCallback(PointEnterTarget);
            DamageablePicker.AddPointExitCallback(PointExitTarget);
            _playerAbilitySystem.AddUseAbilitiesOverHandlers(UseAbilitiesOverHandler);
        }

        private void RemoveHandlers()
        {
            DamageablePicker.RemoveRayFindCallback(FindTarget);
            DamageablePicker.RemovePointEnterCallback(PointEnterTarget);
            DamageablePicker.RemovePointExitCallback(PointExitTarget);
            _playerAbilitySystem.RemoveUseAbilitiesOverHandlers(UseAbilitiesOverHandler);
        }

        private void OnDisable()
        {
            RemoveHandlers();
        }

        private void FindTarget(IDamageable damageable)
        {

            if (CanUse(damageable)) 

            if (!_isCanExecute) return;

            if(_playerAbilitySystem.IsMovementActive) return;
            
            if (ReferenceEquals(damageable, null)) return;

            var direction = damageable.Position - _playerAbilitySystem.Position;

            _characterRotation.SetTargetPointToRotate(damageable.Position);
            if (VisionGameUnit.TryLookAtTarget<EnemyAI>(_playerAbilitySystem.Position, direction))

            {
                _playerAbilitySystem.TryCastActiveAbility(damageable);
            }
        }

        private void PointEnterTarget(IDamageable damageable)
        { 
            if (CanUse(damageable))
            {
                _playerAbilitySystem.PointEnter(damageable);
            }
        }

        private void PointExitTarget(IDamageable damageable)
        {
            if (ReferenceEquals(damageable, null)) return;

            _playerAbilitySystem.PointExit(damageable);
        }

        private bool CanUse(IDamageable damageable)
        {
            if (!_isCanExecute) 
                return false;

            if (_playerAbilitySystem.IsMovementActive) 
                return false;

            if (ReferenceEquals(damageable, null)) 
                return false;

            var direction = damageable.Position - _playerAbilitySystem.Position;

            if (!VisionGameUnit.TryLookAtTarget<EnemyAI>(_playerAbilitySystem.Position, direction))
                return false;

            return true;
        }
    }
}