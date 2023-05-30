using Stats.Basic.Interface;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.Accuracy
{
    public class AccuracyProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly IBasicStats _basicStats;
        
        public AccuracyProvider(IBasicStats basicStats, SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
            _basicStats = basicStats;
        }
        
        public float Calculate()
        {
            return _sideStatsValueFactory.Create(_basicStats);
        }
    }
}