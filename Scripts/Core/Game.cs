using System.Collections.Generic;
using DNVMVC;
using Libraries.DNV.DNVPool.PoolContainer;
using Libraries.DNV.LevelLoader;
using Libraries.DNV.MVC.Core;
using Anthill.Inject;
using Anthill.Core;
using InputControl;
using Stats.Effect;
using UnityEngine;
using Components.InteractablePicker;
using Components.Player;
using Components.WorldDissolve;
using DNVMVC.Controllers;
using GameState;
using Libraries.DNV.DNVPool.Interface;
using PathGenerator.GridMover;
using Resistance;
using Units;

namespace Core
{
    public class Game : AntAbstractBootstrapper
    {
        [SerializeField] private RootUI _root;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private TransparentWorld _transparentWorld;
        [SerializeField] private Player _player;
        [SerializeField] private GameStateSwitcher _gameStateSwitcher;
        [SerializeField] private ClickEffect _clickEffect;
        [SerializeField] private GridMover _gridMover;

        [SerializeField] private PositionPicker _positionPicker;
        [SerializeField] private InteractablePicker _interactablePicker;
        [SerializeField] private GridSegmentPicker _gridSegmentPicker;
        [SerializeField] private DamageablePicker _damageablePicker;

        [SerializeField] private VisionGameUnit _visionGameUnit;

        
        [SerializeField] private ObjectPoolContainer _objectPoolContainer;
        [SerializeField] private EffectVFXPoolContainer _effectVFXPoolContainer;
        [SerializeField] private AbilityEffectPoolContainer _abilityEffectPool;



        private List<IPoolContainer> _poolContainers;
        private EffectVFXRepository _effectVFXRepository;
        
        
        private ServiceInput _serviceInput;

        private IEnemyTarget _enemyTarget;
        private NegativeEffect _negativeEffect;
        private PositiveEffect _positiveEffect;

        private void CreateModels()
        {
            var ui = DNVUI.Create<MainUI>();
            ui.Create<FpsController>().CreateView(_root);
            ui.Create<InventoryController>().CreateView(_root);
            ui.Create<BattleController>().CreateView(_root);
            ui.Create<PlayerStatsController>().CreateView(_root);
            ui.Create<LoaderScreenController>().CreateView(_root);
            ui.Create<ExitController>().CreateView(_root);
            ui.Create<HintWorldController>().CreateView(_root);
            ui.Create<FightController>().CreateView(_root);
            ui.Create<RoundController>().CreateView(_root);
            ui.Create<DeadController>().CreateView(_root);
            ui.Create<NearDeadController>().CreateView(_root);
        }

        private void Start()
        {
            CreateModels();
            InitializeSystems();
            InitPools();

            _serviceInput.Init();

            DNVUI.Get<MainUI>().GetController<FpsController>().Show();
            DNVUI.Get<MainUI>().GetController<InventoryController>().Show();
            DNVUI.Get<MainUI>().GetController<PlayerStatsController>().Show();
            DNVUI.Get<MainUI>().GetController<ExitController>().Show();
            DNVUI.Get<MainUI>().GetController<HintWorldController>().Show();
            DNVUI.Get<MainUI>().GetController<NearDeadController>().Show();
        }

        private void OnDisable()
        {
            _serviceInput.Disable();
        }

        private void LoadLevel()
        {
            _levelManager.LoadLevel(0);
        }

        private void InitializeSystems()
        {
            AntEngine.Add<Gameplay>(aPriority: 2);
        }

        private void InitPools()
        {
            _poolContainers = new List<IPoolContainer>()
            {
                _objectPoolContainer,
                _effectVFXPoolContainer,
            };

            for (int i = 0; i < _poolContainers.Count; i++)
            {
                _poolContainers[i].InitPools(() => Debug.Log("Inits"));
            }
            
            _abilityEffectPool.InitPools(() =>
            {
                var loadScreen = DNVUI.Get<MainUI>().GetController<LoaderScreenController>();
                loadScreen.Show();

                LoadLevel();

                _levelManager.OnLoaded(() =>
                {
                    loadScreen.Hide();
                    _gridMover.Scan();
                    _gameStateSwitcher.SwitchGameOnRuntime();
                });
            });
        }

        private void Update()
        {
            AntEngine.Execute();
        }

        public override void Configure(IInjectContainer aContainer)
        {
            _enemyTarget = _player;

            _serviceInput = new ServiceInput();
            aContainer.RegisterSingleton(this);
            aContainer.RegisterSingleton(_interactablePicker);
            aContainer.RegisterSingleton(_gridSegmentPicker);
            aContainer.RegisterSingleton(_positionPicker);

            aContainer.RegisterSingleton(_transparentWorld);
            aContainer.RegisterSingleton(_player);
            aContainer.RegisterSingleton(_gameStateSwitcher);
            aContainer.RegisterSingleton(_enemyTarget);
            
            aContainer.RegisterSingleton(_objectPoolContainer);
            aContainer.RegisterSingleton(_effectVFXPoolContainer);
            aContainer.RegisterSingleton(_abilityEffectPool);

            
            aContainer.RegisterSingleton(_serviceInput);
            aContainer.RegisterSingleton(_clickEffect);
            aContainer.RegisterSingleton(_damageablePicker);
            
            aContainer.RegisterSingleton(_visionGameUnit);

            aContainer.RegisterSingleton(new NegativeEffect());
            aContainer.RegisterSingleton(new PositiveEffect());
            
        }
    }
}