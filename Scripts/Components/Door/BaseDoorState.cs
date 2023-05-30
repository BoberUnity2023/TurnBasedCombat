namespace Components.Door.States
{
    public abstract class BaseDoorState
    {  
        protected BaseDoorState()
        {
            
        }

        public abstract void TryOpen();

        public abstract void Start();

        public abstract void Stop();
    }
}

