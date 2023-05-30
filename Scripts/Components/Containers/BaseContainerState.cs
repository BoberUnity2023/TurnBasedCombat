namespace Components.Containers.States
{
    public abstract class BaseContainerState
    {
        protected ISwitchState _switchState;

        protected Container _container;

        protected BaseContainerState(ISwitchState switchState, Container container)
        {
            _switchState = switchState;
            _container = container;
        }

        public abstract void TryOpen();

        public abstract void Start();

        public abstract void Stop();
    }
}
