using System;
using Components.AI;
using Components.Player.Movement;
using DamageAcquisition;
using Pathfinding;
using UnityEngine;

namespace AI
{
    
    [RequireComponent(typeof(DamageAcquisitionSystem), typeof(BattleMarker))]
    public class EnemyAI : MonoBehaviour, IAIBehaviourSwitcher
    {
        [SerializeField] private Seeker _seeker;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CombatGroup _combatGroup;


        private BattleMarker _battleMarker;
        private CharacterRotation _characterRotation;
        private DamageAcquisitionSystem _damageAcquisition;
        private AIBehaviour _aiBehaviour;
        private EnemyMovement _enemyMovement;
        private GameStats _gameStats;
        
        
        public CombatGroup CombatGroup => _combatGroup;
        public DamageAcquisitionSystem DamageAcquisitionSystem => _damageAcquisition;

        private Action<EnemyAI> _died;

        private void OnDestroy()
        {
            _enemyMovement.RemoveHandlers();
        }

        private void Start()
        {
            _combatGroup.AddItemInGroup(this);
            _gameStats = new GameStats();
            
            _damageAcquisition = GetComponent<DamageAcquisitionSystem>();
            _damageAcquisition.Init(_gameStats);
            
            _aiBehaviour = new NeutralAIBehavior(this);
            _characterRotation = new CharacterRotation(_characterController, 360f);
            _enemyMovement = new EnemyMovement(_seeker, _characterController, _characterRotation);
            _battleMarker = GetComponent<BattleMarker>();
            
            AddHandlers();
        }

        private void Update()
        {
            _characterRotation.Execute();
            _enemyMovement.Execute();
        }

        private void OnDisable()
        {
            RemoveHandlers();
        }

        public void SwitchAIBehaviour<T>() where T : AIBehaviour
        {
            
        }

        public void AddHealthOverHandler(Action<EnemyAI> dieHandler)
        {
            _died += dieHandler;
        }
        
        public void RemoveHealthOverHandler(Action<EnemyAI> dieHandler)
        {
            _died -= dieHandler;
        }

        private void DiedHandler()
        {
            _died?.Invoke(this);
            gameObject.SetActive(false);
        }

        private void AddHandlers()
        {
            _enemyMovement.AddHandlers();
            _damageAcquisition.AddDiedHandlers(DiedHandler);
        }

        private void RemoveHandlers()
        {
            _enemyMovement.RemoveHandlers();
            _damageAcquisition.RemoveDiedHandlers(DiedHandler);
        }


        public void RoundEnd()
        {
            _damageAcquisition.RoundEnd();
        }

        public void BattleBegin()
        {
            _battleMarker.Show();
            gameObject.layer = LayerMask.NameToLayer("Damageable");
        }

        public void BattleCompleted()
        {
            _battleMarker.Hide();
            gameObject.layer = LayerMask.NameToLayer("ICanFight");
        }
    }
}