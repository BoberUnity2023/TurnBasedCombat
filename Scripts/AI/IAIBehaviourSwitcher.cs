namespace Components.AI
{
    public interface IAIBehaviourSwitcher
    {
        public void SwitchAIBehaviour<T>() where T : AIBehaviour;
    }
}