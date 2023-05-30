using System;
using System.Collections.Generic;
using Abilities;
using Components.Grid;
using Components.Player.Movement;
using DamageAcquisition;
using Stats;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace SystemUsingAbility
{
    public class PlayerAbilitySystem : MonoBehaviour, IOwnerSystemUsingAbility
    {
        [SerializeField] private List<ActiveAbility> _activeAbilities;
        [SerializeField] private List<PassiveAbility> _passiveAbilities;

        private SideStats _sideStats;
        private SystemUsingActiveAbility _systemUsingActiveAbility;
        private SystemUsingMoveAbility _systemUsingMoveAbility;
        private SystemUsingPassiveAbility _systemUsingPassiveAbility;
        private DamageAcquisitionSystem _damageAcquisitionSystem;

        private Action _useAbilitiesOver;
        private bool _isMovementActive;

        public SystemUsingActiveAbility SystemUsingActiveAbility => _systemUsingActiveAbility;
        public Vector3 Position => transform.position;

        public IDamageable Damageable => _damageAcquisitionSystem;

        public List<PassiveAbility> PassiveAbilities { get => _passiveAbilities; set => _passiveAbilities = value; }

        public bool IsMovementActive => _isMovementActive;        

        public void Init(SideStats sideStats, TurnBaseMovement turnBaseMovement, GridSegmentGenerator gridSegmentGenerator, DamageAcquisitionSystem damageAcquisitionSystem)
        {
            _sideStats = sideStats;
            _systemUsingActiveAbility = new SystemUsingActiveAbility(_activeAbilities, _sideStats.ActionPoints, this, _sideStats.Accuracy);
            _systemUsingMoveAbility = new SystemUsingMoveAbility(sideStats.MoveActionPoints, turnBaseMovement, gridSegmentGenerator);
            _systemUsingPassiveAbility = new SystemUsingPassiveAbility(_passiveAbilities, this, _sideStats);
            _damageAcquisitionSystem = damageAcquisitionSystem;

            AddHandlers();
        }           

        public bool TryCastActiveAbility(IDamageable damageable)
        {
            if (_isMovementActive) return false;

            return _systemUsingActiveAbility.GetAbility().TryCast(damageable);
        }

        public void PointEnter(IDamageable damageable)
        {
            if (_isMovementActive) return;

            _systemUsingActiveAbility.GetAbility().PointEnter(damageable);
        }

        public void PointExit(IDamageable damageable)
        {
            if (_isMovementActive) return;

            _systemUsingActiveAbility.GetAbility().PointExit(damageable);
        }

        public void AddUseAbilitiesOverHandlers(Action useAbilitiesOver)
        {
            _useAbilitiesOver += useAbilitiesOver;
        }

        public void RemoveUseAbilitiesOverHandlers(Action useAbilitiesOver)
        {
            _useAbilitiesOver -= useAbilitiesOver;
        }

        public void ChangeCurrentAbility<TK>(TK ability) where TK : ActiveAbility
        {
            _systemUsingActiveAbility.ChangeCurrentAbility(ability);
        }
        
        public void ChangeCurrentAbility<TK>() where TK : ActiveAbility
        {
            _systemUsingActiveAbility.ChangeCurrentAbility<TK>();
        }

        public void RoundEnd()
        {
            _sideStats.RoundEnd();
            _systemUsingActiveAbility.RoundEnd();
            _systemUsingPassiveAbility.RoundEnd();
        }   

        public void BattleCompleted()
        {
            _systemUsingMoveAbility.Stop();
            RemoveMoveDistanceOutline();
            _sideStats.BattleCompleted();
            _systemUsingPassiveAbility.BattleCompleted();
        }
        
        
        private void AddHandlers()
        {
            _systemUsingActiveAbility.AddAbilityCompleteHandler(AbilityCompletedHandler);
            _systemUsingMoveAbility.AddAbilityCompleteHandler(AbilityCompletedHandler);
            _systemUsingMoveAbility.AddHandlers();

            _damageAcquisitionSystem.DamageTaken += DamageTakenHandler;
        }

        private void RemoveHandlers()
        {
            _systemUsingActiveAbility.RemoveAbilityCompleteHandler(AbilityCompletedHandler);
            _systemUsingMoveAbility.RemoveAbilityCompleteHandler(AbilityCompletedHandler);
            _systemUsingMoveAbility.RemoveHandlers();
            _damageAcquisitionSystem.DamageTaken -= DamageTakenHandler;
        }


        private void OnDisable()
        {
            RemoveHandlers();
        }

        private void AbilityCompletedHandler()
        {
            if (_sideStats.ActionPoints.Value <= 0)
            {
                // _useAbilitiesOver?.Invoke();
            }
        }

        public void ChangeActiveAbilityOnMoveAbility()
        {
            _isMovementActive = true;
            _systemUsingMoveAbility.Start();
        }

        public void ChangeMoveAbilityOnActiveAbility()
        {
            _isMovementActive = false;
            _systemUsingMoveAbility.Stop();
        }

        public void GenerateMoveDistanceOutline()
        {
            _systemUsingMoveAbility.GenerateOutline();
        }

        public void RemoveMoveDistanceOutline()
        {
            _systemUsingMoveAbility.RemoveOutline();
        }

        private void DamageTakenHandler(float damage)
        {
            foreach (var ability in SystemUsingActiveAbility.Abilities)
            {
                if (ability.UseAccelerator)
                {
                    ability.Accelerator += damage;
                }
            }
        }
#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                _sideStats.HealthPoints.Reduce(3);
                Debug.Log($"KeyCode.Keypad6");
            }
        }
#endif
    }
}