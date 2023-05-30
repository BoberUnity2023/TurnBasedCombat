using System;
using Anthill.Inject;
using GameState;
using InputControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.InteractablePicker
{
    [RequireComponent(typeof(RayPicker))]
    public class PositionPicker : MonoBehaviour , ISwitchWhenGameStateChanges
    {
        [SerializeField] private LayerMask _layerMask;
        
        private RayPicker _rayPicker;
        private bool _isCanExecute;

        private Vector3 _position = Vector3.zero;
        
        private Action<Vector3> _pathFinding;
        
        [Inject] public ServiceInput ServiceInput { get; set; }
        [Inject] public GameState.GameStateSwitcher GameStateSwitcher { get; set; }

        private void Start()
        {
            AntInject.Inject(this);
            _rayPicker = GetComponent<RayPicker>();
            GameStateSwitcher.TryAdd(this);
        }

        private void OnDisable()
        {
            GameStateSwitcher.TryRemove(this);
        }

        private void FixedUpdate()
        {
            if (!_isCanExecute || EventSystem.current.IsPointerOverGameObject()) return;

            TryFindTheObjectInteraction();
        }

        private void Update()
        {
            if (!ServiceInput.MouseInput.LeftButtonMouse.IsPressed || 
                !_isCanExecute || 
                EventSystem.current.IsPointerOverGameObject()) return;

            _pathFinding?.Invoke(_position);
            
        }

        private void TryFindTheObjectInteraction()
        {
            if (_rayPicker.TryPickObjectPosition(out Vector3 position, _layerMask))
            {
                _position = position;
            }
        }

        public void AddRayFindCallback(Action<Vector3> callback)
        {
            _pathFinding += callback;
        }
        
        public void RemoveRayFindCallback(Action<Vector3> callback)
        {
            _pathFinding -= callback;
        }

        public void SwitchOnTurnBase()
        {
            _isCanExecute = false;
        }

        public void SwitchOnRuntime()
        {
            _isCanExecute = true;
        }
    }
}