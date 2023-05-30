using System;
using Anthill.Inject;
using DamageAcquisition;
using GameState;
using InputControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.InteractablePicker
{
    [RequireComponent(typeof(RayPicker))]
    public class DamageablePicker : MonoBehaviour, ISwitchWhenGameStateChanges
    {
        [SerializeField] private LayerMask _layerMask;
        
        private RayPicker _rayPicker;
        private bool _isCanExecute;

        private IDamageable _damageable;
        private IDamageable _damageablePrevious;

        private Action<IDamageable> _damageableFinding;
        private Action<IDamageable> _damageablePointEnter;
        private Action<IDamageable> _damageablePointExit;

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

            _damageableFinding?.Invoke(_damageable);
        }

        private void TryFindTheObjectInteraction()
        {
            _damageable = _rayPicker.TryPickObjectByType(out IDamageable damageable, _layerMask) ? damageable : null;
            
            if (_damageable != _damageablePrevious)
            {
                if (_damageable != null)
                    _damageablePointEnter?.Invoke(_damageable);

                if (_damageablePrevious != null)
                    _damageablePointExit?.Invoke(_damageablePrevious);

                _damageablePrevious = _damageable;
            }
        }

        public void AddRayFindCallback(Action<IDamageable> callback)
        {
            _damageableFinding += callback;
        }
        
        public void RemoveRayFindCallback(Action<IDamageable> callback)
        {
            _damageableFinding -= callback;
        }

        public void AddPointEnterCallback(Action<IDamageable> callback)
        {
            _damageablePointEnter += callback;
        }

        public void RemovePointEnterCallback(Action<IDamageable> callback)
        {
            _damageablePointEnter -= callback;
        }

        public void AddPointExitCallback(Action<IDamageable> callback)
        {
            _damageablePointExit += callback;
        }

        public void RemovePointExitCallback(Action<IDamageable> callback)
        {
            _damageablePointExit -= callback;
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