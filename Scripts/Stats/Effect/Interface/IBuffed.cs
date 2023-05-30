using Stats.Effect;
using Stats.SideStatsProvider.HealthPoint;

namespace Stats.Side
{
    public interface IBuffed
    {
        public void AddEffect(SideStatProviderDecorator decorator);
        public void RemoveEffect(SideStatProviderDecorator decorator);
    }
}