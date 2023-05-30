using System;
using Anthill.Inject;
using Components.Grid;
using Components.InteractablePicker;
using Components.Player;
using DNVMVC;
using SystemUsingAbility;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.HudView.NextRoundView
{
    public class NextRoundView : UIElement, IView
    {
        [SerializeField] private Button _button;

        private PlayerAbilitySystem _playerAbilitySystem; 
        private bool _isHasHandlers;
        private bool _isInited;
        public Action Clicked;

        [Inject] public Player Player { get; set; }
        [Inject] public GridSegmentPicker GridSegmentPicker { get; set; }

        private void Init()
        {
            _isInited = true;
            AntInject.Inject(this);
            GridSegmentPicker.AddGridSegmentFindCallback(MoveAbilityStartHandler);
            Hide();
            _playerAbilitySystem = Player.GetComponent<PlayerAbilitySystem>();
        }

        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            if (_isInited)
            { 
                GridSegmentPicker.RemoveGridSegmentFindCallback(MoveAbilityStartHandler); 
            }
        }

        public void Show()
        {
            AddHandlers();
            Activate = true;
        }

        public void Hide()
        {            
            RemoveHandlers();
            Activate = false;
        }

        private void AddHandlers()
        {
            if (_isHasHandlers) return;
            _isHasHandlers = true;

            _button.onClick.AddListener(OneClickHandler);
        }

        private void RemoveHandlers()
        {
            if (!_isHasHandlers) return;
            _isHasHandlers = false;

            _button.onClick.RemoveAllListeners();
        }

        private void OneClickHandler()
        {
            Clicked?.Invoke();            
        }

        private void MoveAbilityStartHandler(GridSegment _gridSegment)
        {
            if (_playerAbilitySystem.IsMovementActive)
            {
                Player.CurrentMovement.AddPathCompletedCallback(MoveAbilityStopHandler);
                Hide();
            }
        }

        private void MoveAbilityStopHandler()
        {
            Show();
            Player.CurrentMovement.RemovePathCompletedCallback(MoveAbilityStopHandler);
        }
    }
}