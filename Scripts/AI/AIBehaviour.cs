namespace Components.AI
{
    public abstract class AIBehaviour
    {
        private IAIBehaviourSwitcher _aiBehaviourSwitcher;

        protected AIBehaviour(IAIBehaviourSwitcher aiBehaviourSwitcher)
        {
            _aiBehaviourSwitcher = aiBehaviourSwitcher;
        }
        
        public abstract void Start();
        public abstract void Stop();
    }
}