using System;
using Anthill.Inject;
using Components.Grid;
using GameState;
using InputControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.InteractablePicker
{
    [RequireComponent(typeof(RayPicker))]
    public class GridSegmentPicker : MonoBehaviour, ISwitchWhenGameStateChanges
    {
        [SerializeField] private LayerMask _layerMask;
        private RayPicker _rayPicker;
        private bool _isCanExecute;
        private bool _isNewGridSegment;

        private Action<GridSegment> _pathFinding;

        private GridSegment _gridSegment;
        
        [Inject] public ServiceInput ServiceInput { get; set; }
        [Inject] public GameState.GameStateSwitcher GameStateSwitcher { get; set; }

        private void Start()
        {
            AntInject.Inject(this);
            _rayPicker = GetComponent<RayPicker>();
            GameStateSwitcher.TryAdd(this);
        }
        
        private void FixedUpdate()
        {
            if (!_isCanExecute || EventSystem.current.IsPointerOverGameObject()) return;

            TryFindTheObjectInteraction();
        }

        private void Update()
        {
            if (!ServiceInput.MouseInput.LeftButtonMouse.IsPressed || !_isCanExecute || EventSystem.current.IsPointerOverGameObject()) return;
            
            _pathFinding?.Invoke(_gridSegment);
        }

        private void TryFindTheObjectInteraction()
        {
            if (_rayPicker.TryPickObjectByType(out GridSegment gridSegment, _layerMask))
            {
                _gridSegment = gridSegment;
            }
            else
            {
                _gridSegment = null;
            }
        }

        public void AddGridSegmentFindCallback(Action<GridSegment> callback)
        {
            _pathFinding += callback;
        }
        
        public void RemoveGridSegmentFindCallback(Action<GridSegment> callback)
        {
            _pathFinding -= callback;
        }

        public void SwitchOnTurnBase()
        {
            _isCanExecute = true;
        }

        public void SwitchOnRuntime()
        {
            _isCanExecute = false;
        }
    }
}