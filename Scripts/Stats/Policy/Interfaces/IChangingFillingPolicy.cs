namespace Stats.Policy.Interfaces
{
    public interface IChangingFillingPolicy
    {
        public void ChangePolicy(IPolicyThatStatsIsFilled policyThatStatsIsFilled);
    }
}