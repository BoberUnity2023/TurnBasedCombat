using Stats.Basic;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.Initiative
{
    public class InitiativeProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly Level _level;
        
        public InitiativeProvider(Level level, SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
            _level = level;
        }
        
        public float Calculate()
        {
            return _sideStatsValueFactory.Create(_level);
        }
    }
}