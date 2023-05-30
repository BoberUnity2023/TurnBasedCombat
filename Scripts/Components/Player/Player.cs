using System.Collections.Generic;
using System.Linq;
using Anthill.Inject;
using Combat;
using Components.Camera.PlayerCamera;
using Components.Grid;
using Components.Player.Movement;
using DamageAcquisition;
using DNVMVC;
using DNVMVC.Controllers;
using GameState;
using Libraries.DNV.MVC.Core;
using Pathfinding;
using PathGenerator.GridMover;
using UnityEngine;

namespace Components.Player
{
    [RequireComponent(typeof(PlayerCombatSystem), typeof(DamageAcquisitionSystem))]
    public class Player : MonoBehaviour, ICameraTarget, ISwitchWhenGameStateChanges, IEnemyTarget
    {
        [SerializeField] private Seeker _seeker;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private SingleNodeBlocker _singleNodeBlocker;
        [SerializeField] private BlockManager _blockManager;


        [SerializeField] private GridMover _gridMover;
        [SerializeField] private PlayerCamera _playerCamera;

        [SerializeField] private LineRenderer _lineRenderer;

        private PlayerCombatSystem _playerCombatSystem;
        private DamageAcquisitionSystem _damageAcquisitionSystem;

        private IPlayerMovement _currentMovement;
        private List<IPlayerMovement> _allMovement;
        private CharacterRotation _characterRotation;
        private GameStats _gameStats;

        private GridSegmentGenerator _gridSegmentGenerator;

        public Transform Target => transform;
        public GameStats GameStats => _gameStats;

        public IPlayerMovement CurrentMovement => _currentMovement;


        [Inject] public GameStateSwitcher GameStateSwitcher { get; set; }


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            AntInject.Inject(this);

            _gameStats = new GameStats();


            _damageAcquisitionSystem = GetComponent<DamageAcquisitionSystem>();
            _damageAcquisitionSystem.Init(_gameStats);


            _characterRotation = new CharacterRotation(_characterController, 580f);

            var traversalProvider = new BlockManager.TraversalProvider(_blockManager,
                BlockManager.BlockMode.AllExceptSelector,
                new List<SingleNodeBlocker>() { _singleNodeBlocker });

            _gridSegmentGenerator = new GridSegmentGenerator(transform, traversalProvider, _lineRenderer);


            var turnBase = new TurnBaseMovement(6f, _characterController,
                _gameStats.SideStats.MoveActionPoints,
                _characterRotation,
                _seeker, _gridSegmentGenerator);

            _allMovement = new List<IPlayerMovement>()
            {
                new RuntimeMovement(_seeker, _characterController, _characterRotation),
                turnBase,
            };

            _playerCombatSystem = GetComponent<PlayerCombatSystem>();
            _playerCombatSystem.Init(_gameStats.SideStats, _damageAcquisitionSystem, turnBase, _gridSegmentGenerator, _characterRotation);

            _currentMovement = _allMovement[0];
            _currentMovement.Start();

            _gridMover.Init(this);
            _gridMover.StartMove();

            GameStateSwitcher.TryAdd(this);


            _playerCamera.Init(this);

            AddHandlers();

            DNVUI.Get<MainUI>().GetController<BattleController>().Init().Show();
        }

        private void OnDisable()
        {
            RemoveHandlers();
        }

        private void AddHandlers()
        {
            foreach (var playerMovement in _allMovement)
            {
                playerMovement.AddHandlers();
            }
        }

        private void RemoveHandlers()
        {
            foreach (var playerMovement in _allMovement)
            {
                playerMovement.RemoveHandlers();
            }
        }

        private void ChangeMovement<T>() where T : IPlayerMovement
        {
            var movement = _allMovement.FirstOrDefault(x => x is T);
            _currentMovement.Stop();
            movement.Start();
            _currentMovement = movement;
        }

        private void Update()
        {
            _playerCamera.Execute();
            _currentMovement.Execute();
            _characterRotation.Execute();
            _gridMover.Execute();
        }


        public void SwitchOnTurnBase()
        {
            ChangeMovement<TurnBaseMovement>();
        }

        public void SwitchOnRuntime()
        {
            ChangeMovement<RuntimeMovement>();
        }
    }
}