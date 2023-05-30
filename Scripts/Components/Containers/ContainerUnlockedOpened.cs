using Components.Containers.States;
using UnityEngine;

namespace Components.Containers
{
    public class ContainerUnlockedOpened : BaseContainerState
    {
        public ContainerUnlockedOpened(ISwitchState switchState, Container container) : base(switchState, container)
        {
        }

        public override void TryOpen()
        {
            if (_container.HasItems)
            {
                _container.Animator.Play("Close");
                _switchState.SwitchState<ContainerUnlockedClosed>();
            }
            else
                Debug.Log("ContainerUnlockedOpened Has no items");
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }
    }
}
