using InputControl.Template;

namespace InputControl
{
    public class ServiceInput 
    {
        private GameInput _gameInput;

        private CameraInput _cameraInput;
        private KeyboardInput _keyboardInput;
        private MouseInput _mouseInput;

        public MouseInput MouseInput => _mouseInput;
        public CameraInput CameraInput => _cameraInput;
        public KeyboardInput KeyboardInput => _keyboardInput;

        public void Init()
        {
            _gameInput = new GameInput();
            
            _cameraInput = new CameraInput()
            {
                Zoom = new InputVerticalModel(_gameInput.Cursor.Zoom),

                MouseDeltaVertical = new InputVerticalModel(_gameInput.Cursor.Delta),
                MouseDeltaHorizontal = new InputHorizontalModel(_gameInput.Cursor.Delta),

                MousePositionVertical = new InputVerticalModel(_gameInput.Cursor.Position),
                MousePositionHorizontal = new InputHorizontalModel(_gameInput.Cursor.Position),

                SwitchCameraStateButton = new InputKeyAndReleaseModel(_gameInput.CameraInput.SwitchCameraState),
                MoveOnStartPositionButton = new InputKeyAndReleaseModel(_gameInput.CameraInput.MoveOnStartPosition),
                RightMouseButton = new InputKeyAndReleaseModel(_gameInput.CameraInput.RightMouseButton)
            };

            _keyboardInput = new KeyboardInput()
            {
                Vertical = new InputVerticalModel(_gameInput.Input.Move),
                Horizontal = new InputHorizontalModel(_gameInput.Input.Move)
            };

            _mouseInput = new MouseInput()
            {
                LeftButtonMouse = new InputKeyAndReleaseModel(_gameInput.MouseButton.LeftButton)
            };
            
                
            _gameInput.Enable();
        }
        
       
        public void Disable()
        {
            _gameInput.Disable();
            _gameInput.Dispose();
        }
    }
}