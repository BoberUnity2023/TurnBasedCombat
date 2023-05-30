using Components.Door.States;

namespace Components.Door
{
    public class DoorUnlockedClosed : BaseDoorState
    {
        private Door _door;        

        public DoorUnlockedClosed(Door door) : base()
        {            
            _door = door;
        }

        public override void TryOpen()
        {            
            _door.Animator.Play("OpenDoor");
            _door.DoorBehaviour.SwitchState<DoorUnlockedOpened>();
        }

        public override void Start()
        {

        }

        public override void Stop()
        {            
            
        }
    }
}