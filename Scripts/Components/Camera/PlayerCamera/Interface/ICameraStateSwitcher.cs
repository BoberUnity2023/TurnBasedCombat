using Components.Camera.PlayerCamera.States;

namespace Components.Camera.PlayerCamera.Interface
{
    public interface ICameraStateSwitcher
    {
        public void SwitchState<T>() where T : BaseCameraState;
    }
}
