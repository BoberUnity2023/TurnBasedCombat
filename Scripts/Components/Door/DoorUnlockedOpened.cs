using Components.Door.States;

namespace Components.Door
{
    public class DoorUnlockedOpened : BaseDoorState
    {
        private Door _door;

        public DoorUnlockedOpened(Door door) : base()
        {
            _door = door;
        }

        public override void TryOpen()
        {
            //Do nothing
        }

        public override void Start()
        {

        }

        public override void Stop()
        {
            
        }
    }
}

