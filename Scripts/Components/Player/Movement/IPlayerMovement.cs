using System;

namespace Components.Player.Movement
{
    public interface IPlayerMovement
    {
        public void Start();
        public void Stop();
        public void Execute();
        public void AddHandlers();
        public void RemoveHandlers();
        public void AddPathCompletedCallback(Action callback);
        public void RemovePathCompletedCallback(Action callback);
    }
}