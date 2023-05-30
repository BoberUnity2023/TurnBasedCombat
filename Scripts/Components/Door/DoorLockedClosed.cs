using UnityEngine;

using Components.Door.States;

namespace Components.Door
{
    public class DoorLockedClosed : BaseDoorState
    {
        private Door _door;

        public DoorLockedClosed(Door door) : base()
        {
            _door = door;
        }

        public override void TryOpen()
        {
            Debug.Log("Door is closed. You need the key");
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }
    }
}

