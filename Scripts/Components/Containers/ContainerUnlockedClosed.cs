using Components.Containers.States;
using UnityEngine;

namespace Components.Containers
{
    public class ContainerUnlockedClosed : BaseContainerState
    {
        public ContainerUnlockedClosed(ISwitchState switchState, Container container) : base(switchState, container)
        {
        }

        public override void TryOpen()
        {            
            _container.Animator.Play("Open");
            _switchState.SwitchState<ContainerUnlockedOpened>();
        }

        public override void Start()
        {
            Debug.Log("Chest Start UnlockedClosed");
        }

        public override void Stop()
        {

        }
    }
}
