using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.ActionPoints
{
    public class ActionPointsProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly IBasicStats _basicStats;
        private readonly MechanicalHands _mechanicalHands;
        
        public ActionPointsProvider(IBasicStats basicStats, MechanicalHands mechanicalHands, SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
            _basicStats = basicStats;
            _mechanicalHands = mechanicalHands;
        }
        
        public float Calculate()
        {
            return _sideStatsValueFactory.Create(_basicStats, _mechanicalHands);
        }
    }
}