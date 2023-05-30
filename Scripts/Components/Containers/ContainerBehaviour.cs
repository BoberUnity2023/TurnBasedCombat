using Components.Containers.States;
using System.Collections.Generic;
using System.Linq;

namespace Components.Containers
{
    public class ContainerBehaviour: ISwitchState
    {
        private BaseContainerState _currentState;
        private readonly List<BaseContainerState> _allStates;

        public ContainerBehaviour(Container _container)
        {
            _allStates = new List<BaseContainerState>()
            {
                new ContainerUnlockedClosed(this, _container),
                new ContainerUnlockedOpened(this, _container),
                new ContainerLockedClosed(this, _container)
            };
            _currentState = _allStates[0];
            _currentState.Start();
        }

        public void SwitchState<T>() where T : BaseContainerState
        {
            var state = _allStates.FirstOrDefault(x => x is T);
            _currentState.Stop();
            state.Start();
            _currentState = state;
        }

        public void TryOpen()
        {
            _currentState.TryOpen();
        }
    }
}
