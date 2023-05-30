using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class InputVerticalModel
    {
        private readonly InputAction _inputAction;
        public float Value => _inputAction.ReadValue<Vector2>().y;
        public InputVerticalModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }
    }
}