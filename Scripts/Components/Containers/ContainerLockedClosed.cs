using UnityEngine;
using Components.Containers.States;

namespace Components.Containers
{
    public class ContainerLockedClosed : BaseContainerState
    {
        public ContainerLockedClosed(ISwitchState switchState, Container container) : base(switchState, container)
        {
        }

        private bool HasKey => true;
        

        public override void TryOpen()
        {            
            if (!HasKey)
            {
                ShowHintClose();
                return;     
            }

            Open();
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }

        private void ShowHintClose()
        {
            Debug.Log("Container is closed. You need the key");
        }

        private void Open()
        {            
            Debug.Log("Container is opening...");
            _switchState.SwitchState<ContainerUnlockedClosed>();
        }
    }
}
