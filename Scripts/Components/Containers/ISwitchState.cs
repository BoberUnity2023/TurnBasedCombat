using Components.Containers.States;

namespace Components.Containers
{
    public interface ISwitchState
    {
        public void SwitchState<T>() where T : BaseContainerState;
    }
}

