namespace Stats.Policy.Interfaces
{
    public interface IPolicyThatStatsIsOver
    {
        public float Value { get; }
        public bool IsOver(float value);
    }
}