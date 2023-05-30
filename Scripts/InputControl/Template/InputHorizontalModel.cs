using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class InputHorizontalModel
    {
        private readonly InputAction _inputAction;
        public float Value => _inputAction.ReadValue<Vector2>().x;
        public InputHorizontalModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }
    }
}