using Components.Door.States;
using System.Collections.Generic;
using System.Linq;

namespace Components.Door
{
    public class DoorBehaviour
    {
        private BaseDoorState _currentState;
        private readonly List<BaseDoorState> _allStates;

        public DoorBehaviour(Door _door)
        {
            _allStates = new List<BaseDoorState>()
            {
                new DoorUnlockedClosed(_door),
                new DoorUnlockedOpened(_door),
                new DoorLockedClosed(_door)
            };
            _currentState = _allStates[0];
            _currentState.Start();
        }

        public void SwitchState<T>() where T : BaseDoorState
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
