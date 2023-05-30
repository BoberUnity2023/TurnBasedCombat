using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.SprintLimit
{
    public class SprintLimitProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        
        public SprintLimitProvider(SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
        }
        
        public float Calculate()
        {
            return _sideStatsValueFactory.Create();
        }
    }
}