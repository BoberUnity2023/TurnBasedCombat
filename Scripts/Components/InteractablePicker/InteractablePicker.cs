using System;
using Anthill.Inject;
using Components.Interactable.Interface;
using GameState;
using InputControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Components.InteractablePicker
{
    [RequireComponent(typeof(RayPicker))]
    public class InteractablePicker : MonoBehaviour, ISwitchWhenGameStateChanges
    {
        [SerializeField] private LayerMask _layerMask;

        private RayPicker _rayPicker;
        private IInteractable _interactable;
        private bool _isCanExecute;

        private Action<Vector3, IInteractable> PathFindingWithOwner;

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

            TryPassThePathWithDestinationObject();
        }

        private void TryPassThePathWithDestinationObject()
        {
            if (!ReferenceEquals(_interactable, null))
            {
                Debug.Log("TryPassThePathWithDestinationObject");
                PathFindingWithOwner?.Invoke(_interactable.FindNearPointToMove().position, _interactable);
            }
        }

        private void TryFindTheObjectInteraction()
        {
            if (_rayPicker.TryPickObjectByType(out IInteractable interactable, _layerMask))
            {
                if (_interactable != interactable)
                {
                    _interactable?.Exit();

                    _interactable = interactable;

                    _interactable?.Enter();
                }
            }
            else
            {
                _interactable?.Exit();
                _interactable = null;
            }
        }

        public void AddInteractiveObjectWillFind(Action<Vector3, IInteractable> callback)
        {
            PathFindingWithOwner += callback;
        }
        
        public void RemoveInteractiveObjectWillFind(Action<Vector3, IInteractable> callback)
        {
            PathFindingWithOwner -= callback;
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